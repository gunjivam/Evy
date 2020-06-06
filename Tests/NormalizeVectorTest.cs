using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           Vector vector1 = new Vector(25, 32);
            sum = 0;

            for (int i = 0; i < vector1.Length; i++)
            {
                sum += vector[i];
            }

            if (sum != 0) {
                vector1.Normalize();
            }
            

            
        }


    }
}

