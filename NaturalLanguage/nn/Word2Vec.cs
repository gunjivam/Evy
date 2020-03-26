using NumSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tensorflow;
using static Tensorflow.Binding;

namespace NaturalLanguage.NN
{
    public class Word2Vec : AbstractNN, INN
    {
        // Training Parameters
        int batch_size = 128;
        int num_steps = 3000; //3000000;
        int display_step = 1000; //10000;

        string[] text_words;
        List<WordId> word2id;
        int[] data;
        HashSet<string> words = new HashSet<string>();

        // Word2Vec Parameters
        int min_occurrence = 10; // Remove all words that does not appears at least n times
        int skip_window = 3; // How many words to consider left and right
        int num_skips = 2; // How many times to reuse an input to generate a label

        int embedded_size = 50;
        int data_index = 0;
        float average_loss = 0;

        string graph_path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), $"Eevee\\Eevee\\bin\\graph2");

        //Tensors
        Tensor X = null;
        Tensor Y = null;
        Tensor loss_op = null;
        Operation train_op = null;
        Tensor embedding = null;
        Tensor cosine_sim_op = null;

        Graph graph = null;

        public Word2Vec()
        {

            PrepareData();

            BuildGraph();
        }


        public Config InitConfig()
            => Config = new Config
            {
                Name = "Word2Vec",
                Enabled = true,
                IsImportingGraph = true
            };


        public override void BuildGraph()
        {
            graph = tf.Graph().as_default();

            tf.train.import_meta_graph(Path.Combine(graph_path, "my-model2.meta"));

            // Input data
            X = graph.OperationByName("Placeholder");
            // Input label
            Y = graph.OperationByName("Placeholder_1");

            embedding = graph.OperationByName("embedding_lookup");

            // Compute the average NCE loss for the batch
            loss_op = graph.OperationByName("Mean");
            // Define the optimizer
            train_op = graph.OperationByName("GradientDescent");
            cosine_sim_op = graph.OperationByName("MatMul_1");
        }

        public override void Train()
        {

            var init = tf.global_variables_initializer();

            using (var sess = tf.Session(graph))
            {

                sess.run(init);

                tf.train.Saver().restore(sess, tf.train.latest_checkpoint(graph_path));


                foreach (var step in range(1, num_steps + 1))
                {
                    // Get a new batch of data
                    var (batch_x, batch_y) = next_batch(batch_size, num_skips, skip_window);

                    (_, float loss) = sess.run((train_op, loss_op), (X, batch_x), (Y, batch_y));
                    average_loss += loss;

                    if (step % display_step == 0 || step == 1)
                    {
                        if (step > 1)
                            average_loss /= display_step;
                        average_loss = 0;
                    }
                }

                tf.train.Saver().save(sess, Path.Combine(graph_path, "my-model2"));

            }
        }

        public bool Run()
        {
            //PrepareData();

            //BuildGraph();

            //Train();

            return true;
        }


        public override float[] Predict(string word)
        {

            if (words.Contains(word)) { 

                //var init = tf.global_variables_initializer();


                using (var sess = tf.Session(graph))
                {

                    //sess.run(init);

                    tf.train.Saver().restore(sess, tf.train.latest_checkpoint(graph_path));

                    var x_test = (from w in new String[] { word }
                                  join id in word2id on w equals id.Word into wi
                                  from wi2 in wi.DefaultIfEmpty()
                                  select wi2 == null ? 0 : wi2.Id).ToArray();

                    var emb = sess.run(embedding, (X, x_test));

                    //float[] l = emb[0].Data<float>().Skip(1).ToArray();

                    //var sim = sess.run(cosine_sim_op, (X, x_test));

                    return NaturalLanguage.vector.VectorSpace.Normalize(emb[0].Data<float>().ToArray());
                }
            }
            else
            {
                return new float[embedded_size];
            }

        }

        public override float[] PredictText(string text)
        {
            string[] words = Text.RemoveStopWords.RemoveWords(text.Trim().ToLower());

            float[][] vectors = new float[words.Length][];

            for (int i = 0; i < words.Length; i++)
            {
                vectors[i] = Predict(words[i]);
            }

            // Parallel.ForEach(words, word =>
            //{
            //    vector = NaturalLanguage.vector.VectorSpace.Add(vector, model.Predict(word));
            //});

            return vector.VectorSpace.Normalize(vector.VectorSpace.Add(vectors));
        }


        // Generate training batch for the skip-gram model
        private (NDArray, NDArray) next_batch(int batch_size, int num_skips, int skip_window)
        {
            var batch = np.ndarray(new Shape(batch_size), dtype: np.int32);
            var labels = np.ndarray((batch_size, 1), dtype: np.int32);
            // get window size (words left and right + current one)
            int span = 2 * skip_window + 1;
            var buffer = new Queue<int>(span);
            if (data_index + span > data.Length)
                data_index = 0;
            data.Skip(data_index).Take(span).ToList().ForEach(x => buffer.Enqueue(x));
            data_index += span;

            foreach (var i in range(batch_size / num_skips))
            {
                var context_words = range(span).Where(x => x != skip_window).ToArray();
                var words_to_use = new int[] { 1, 6 };
                foreach (var (j, context_word) in enumerate(words_to_use))
                {
                    batch[i * num_skips + j] = buffer.ElementAt(skip_window);
                    labels[i * num_skips + j, 0] = buffer.ElementAt(context_word);
                }

                if (data_index == len(data))
                {
                    //buffer.extend(data[0:span]);
                    data_index = span;
                }
                else
                {
                    buffer.Enqueue(data[data_index]);
                    data_index += 1;
                }
            }

            // Backtrack a little bit to avoid skipping words in the end of a batch
            data_index = (data_index + len(data) - span) % len(data);

            return (batch, labels);
        }

        public override void PrepareData()
        {
            int wordId = 0;

            text_words = File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "text.txt"), Encoding.UTF8).Split(' ');

            // Build the dictionary and replace rare words with UNK token
            word2id = text_words.GroupBy(x => x)
                .Select(x => new WordId
                {
                    Word = x.Key,
                    Occurrence = x.Count()
                })
                .Where(x => (x.Occurrence >= min_occurrence) && (x.Word.Length > 2)) // Remove samples with less than 'min_occurrence' occurrences
                .OrderByDescending(x => x.Occurrence) // Retrieve the most common words
                .Select(x => new WordId
                {
                    Word = x.Word,
                    Id = ++wordId, // Assign an id to each word
                    Occurrence = x.Occurrence
                })
                .ToList();

            // Retrieve a word id, or assign it index 0 ('UNK') if not in dictionary
            data = (from word in text_words
                    join id in word2id on word equals id.Word into wi
                    from wi2 in wi.DefaultIfEmpty()
                    select wi2 == null ? 0 : wi2.Id).ToArray();


            foreach(string word in text_words)
            {
                words.Add(word);
            }

            word2id.Insert(0, new WordId { Word = "UNK", Id = 0, Occurrence = data.Count(x => x == 0) });

        }

        public override int GetOutputSize()
        {
            return embedded_size;
        }

        private class WordId
        {
            public string Word { get; set; }
            public int Id { get; set; }
            public int Occurrence { get; set; }

            public override string ToString()
            {
                return Word + " " + Id + " " + Occurrence;
            }
        }
    }
}