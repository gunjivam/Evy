using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vspace = NaturalLanguage.vector.VectorSpace;
using W2V = NaturalLanguage.NN.Word2Vec;

namespace Tests
{
    [TestClass]
    public class NaturalLanguageTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            float[] v1 = { 1.0f, 2.0f, 3.0f };
            float[] v2 = { 1.0f, 2.0f, 3.0f };

            float[] v3 = NaturalLanguage.vector.VectorSpace.Add(v1, v2);

            CollectionAssert.AreEqual(v3, Vspace.Scale(2, v1));
            CollectionAssert.AreEqual(Vspace.Negate(v1), Vspace.Scale(-1, v1));
            Assert.AreEqual(Vspace.DotProduct(v2, v2), 14);
            Assert.AreEqual(Vspace.Magnitude(v2), Math.Sqrt(14), 0.01);
            CollectionAssert.AreEqual(Vspace.Normalize(v1), new float[] { 1 / (float)Math.Sqrt(14), 2 / (float)Math.Sqrt(14), 3 / (float)Math.Sqrt(14)});

            float[] italian_vec = { -3.1635818f, -0.20904206f, 0.29893237f, -0.00035927136f, -0.058570296f, -0.00846739f, 0.3034302f, -0.016199231f, -0.27490035f, -0.1588173f };
            float[] anthem_vec = { -2.863994f, 0.09603188f, 0.42992353f, 0.0986715f, -0.15630788f, -0.06696623f, 0.43981647f, 0.18234879f, -0.480734f, -0.04069173f };
            
            float[] niv = Vspace.Normalize(italian_vec);

            W2V w = new W2V();

            float[] predicted = w.PredictText("italian");

            //CollectionAssert.AreEqual(predicted, niv);

            float[] niv2= Vspace.Normalize(Vspace.Add(italian_vec, anthem_vec));

            string[] ws = NaturalLanguage.Text.RemoveStopWords.RemoveWords("anthem".Trim().ToLower());
            Assert.AreEqual("anthem", ws[0]);

            predicted = w.PredictText("anthem");
            CollectionAssert.AreEqual(predicted, Vspace.Normalize(anthem_vec));

            predicted = w.PredictText("italian anthem");
            CollectionAssert.AreEqual(predicted, niv2);

            Assert.IsFalse(predicted[0] == 0);
            Assert.IsFalse(niv[0] == 0);

            float[] german_vec = { -1.1745248f, -0.084007405f, 0.034549948f, -0.16554825f, -0.14128838f, 0.020536855f, -0.08310235f, 0.13850354f, -0.09974682f, 0.05751427f };

            anthem_vec = new float[]{ -2.863994f, 0.09603188f, 0.42992353f, 0.0986715f, -0.15630788f, -0.06696623f, 0.43981647f, 0.18234879f, -0.480734f, -0.04069173f };

            float[] niv3 = Vspace.Normalize(Vspace.Add(german_vec, anthem_vec));

            float[] predicted2 = w.PredictText("german anthem");
            CollectionAssert.AreEqual(predicted2, niv3);

            Assert.IsFalse(predicted2[0] == 0);
            Assert.IsFalse(niv3[0] == 0);

            Assert.IsTrue(Vspace.Loss(w.PredictText("german anthem"), w.PredictText("italian anthem")) < Vspace.Loss(w.PredictText("italian anthem"), w.PredictText("fluffy pancakes")));
        }
    }
}
