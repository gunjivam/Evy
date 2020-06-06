using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector vector1 = new Vector(25, 30);
            Vector vector2 = new Vector(22, 29);
            Vector sum = new Vector();

            sum = Vector.Add(vector1, vector2);

            return vector1;





        }


    }
}

