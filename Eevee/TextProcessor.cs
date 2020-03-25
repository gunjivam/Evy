using System.Threading.Tasks;

namespace Eevee
{
    public sealed class TextProcessor : ITP
    {
        private readonly NaturalLanguage.NN.INN model;

        public TextProcessor()
        {
            model = new NaturalLanguage.NN.Word2Vec();
        }

        public string Predict(string text)
        {
            string[] words = NaturalLanguage.Text.RemoveStopWords.RemoveWords(text.Trim().ToLower());

            float[] vector = new float[model.GetOutputSize()];

            float[][] vectors = new float[words.Length][];

            for(int i = 0; i <  words.Length; i++)
            {
                vectors[i] = model.Predict(words[i]);
            }

            vector = NaturalLanguage.vector.VectorSpace.Add(vectors);

           // Parallel.ForEach(words, word =>
           //{
           //    vector = NaturalLanguage.vector.VectorSpace.Add(vector, model.Predict(word));
           //});

            vector = NaturalLanguage.vector.VectorSpace.Normalize(vector);

            return NaturalLanguage.vector.VectorSpace.ToString(vector);
        } 

        public float Loss(float[] v1, float[] v2)
        {
            return NaturalLanguage.vector.VectorSpace.Loss(v1, v2);
        }

        public float[] ToArray(string vector)
        {
            return NaturalLanguage.vector.VectorSpace.ToArray(vector);
        }

        public string ToString(float[] vector)
        {
            return NaturalLanguage.vector.VectorSpace.ToString(vector);
        }
    }
}
