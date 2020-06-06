using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector vector1 = new Vector { 29, 50 };
           Vector vector2 = new Vector { 47, 91 };

            Vector loss1 = new Vector();

            loss1 = Vector.Loss1(vector1, vector2);



        


    }
}

