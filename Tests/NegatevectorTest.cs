using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector vector1 = new Vector(27, 50);
            
            Vector negate = new Vector();

            negate = vector1.Negate();

            return vector1;





        }


    }
}

