using System;
using System.Collections.Generic;
using Microsoft.ML;

namespace NaturalLanguage.Text
{
    public static class Tokenizer
    {

        public static string[] Tokenize(string text)
        {
            var mlContext = new MLContext();

            var emptySamples = new List<TextData>();

            var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

            var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words",
                "Text", separators: new[] { ' ', ',', '\'', '.', '?', ';', '!' });

            var textTransformer = textPipeline.Fit(emptyDataView);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
                TransformedTextData>(textTransformer);



            // Call the prediction API to convert the text into words.
            var data = new TextData()
            {
                Text = text
            };

            var prediction = predictionEngine.Predict(data);

            // Print the length of the word vector.
            Console.WriteLine($"Number of words: {prediction.Words.Length}");

            // Print the word vector.
            //Console.WriteLine($"\nWords: {string.Join(",", prediction.Words)}");

            return prediction.Words;
        }

        public class TextData
        {
            public string Text { get; set; }
        }

        public class TransformedTextData : TextData
        {
            public string[] Words { get; set; }
        }
    }
}