using System;
using System.Collections.Generic;
using Microsoft.ML;

namespace NaturalLanguage.Text
{
    public static class RemoveStopWords
    {
        public static string[] RemoveWords(string text)
        {
            var mlContext = new MLContext();

            var emptySamples = new List<TextData>();

            var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

            var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words",
                "Text")
                .Append(mlContext.Transforms.Text.RemoveStopWords(
                "WordsWithoutStopWords", "Words", stopwords:
                new[] { "a", "about", "above", "above", "across",
                    "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also","although","always","am","among", "amongst", "amoungst",
                    "amount",  "an", "and", "another", "any","anyhow","anyone","anything","anyway", "anywhere", "are", "around", "as",  "at", "back","be","became", "because",
                    "become","becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between", "beyond", "bill", "both", "bottom",
                    "but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt", "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "each",
                    "eg", "eight", "either", "eleven","else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything", "everywhere", "except", "few",
                    "fifteen", "fify", "fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give",
                    "go", "had", "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "him", "himself", "his", "how",
                    "however", "hundred", "ie", "if", "in", "inc", "indeed", "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less",
                    "ltd", "made", "many", "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", "myself", "name",
                    "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "off", "often",
                    "on", "once", "one", "only", "onto", "or", "other", "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own","part", "per", "perhaps", "please",
                    "put", "rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious", "several", "she", "should", "show", "side", "since", "sincere", "six",
                    "sixty", "so", "some", "somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten", "than", "that",
                    "the", "their", "them", "themselves", "then", "thence", "there", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", "they", "thick",
                    "thin", "third", "this", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve",
                    "twenty", "two", "un", "under", "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever",
                    "where", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "whoever", "whole", "whom",
                    "whose", "why", "will", "with", "within", "without", "would", "yet", "you", "your", "yours", "yourself", "yourselves", "the", "s", "seven", "zero", "i",
                }));

            var textTransformer = textPipeline.Fit(emptyDataView);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
                TransformedTextData>(textTransformer);

            Stemmer s = new Stemmer();

            string[] tokenized_text = Tokenizer.Tokenize(text);
            int t_l = tokenized_text.Length;
            for (int i = 0; i < t_l; i++)
            {
                tokenized_text[i] = s.stem(tokenized_text[i]);
            }

            text = string.Join(" ", tokenized_text);

            // Call the prediction API to remove stop words.
            var data = new TextData()
            {
                Text = text
            };

            var prediction = predictionEngine.Predict(data);

            // Print the length of the word vector after the stop words removed.
            Console.WriteLine("Number of words: " + prediction.WordsWithoutStopWords
                .Length);

            //System.IO.File.WriteAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "text.txt"), string.Join(" ", prediction.WordsWithoutStopWords));

            // Print the word vector without stop words.
            //Console.WriteLine("\nWords without stop words: " + string.Join(",",
            //    prediction.WordsWithoutStopWords));
            //  Expected output:
            //   Number of words: 14
            //   Words without stop words: ML.NET's,RemoveStopWords,API,removes,stop,words,text/string,using,list,of,stop,words,provided,user.

            return prediction.WordsWithoutStopWords;
        }

        public class TextData
        {
            public string Text { get; set; }
        }

        public class TransformedTextData : TextData
        {
            public string[] WordsWithoutStopWords { get; set; }
        }
    }
}
