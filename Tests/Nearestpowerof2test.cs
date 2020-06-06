using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 10;

            if (x <= 0)
            {
                return 0;
            }

            else {
                x.nearest_power_2();
            }




        }


    }
}

