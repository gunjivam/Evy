using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector vec1 = new Vector(23, 42);
            Point v = new Point(61, 92);
            Point sum = new Point();

            sum = Vector.Add(v, vec1);

            return sum;





        }


    }
}

