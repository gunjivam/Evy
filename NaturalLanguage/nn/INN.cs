using Tensorflow;

namespace NaturalLanguage.NN
{
    public interface INN
    {
        Config Config { get; set; }
        Config InitConfig();
        bool Run();

        void Train();
        string FreezeModel();
        void Test();

        float[] Predict(string word);

        float[] PredictText(string text);

        Graph ImportGraph();

        void BuildGraph();

        void PrepareData();

        int GetOutputSize();
    }
}
