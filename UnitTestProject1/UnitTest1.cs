using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollCalculation;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MainWindow window = new MainWindow();

        [TestMethod]
        public void TestMethod_CountOfHours_Zero()
        {
            Assert.IsNotNull(window.Calculate(150, 0, true));
        }

        [TestMethod]
        public void TestMethod_CountOfHours_Pozitive()
        {
            Assert.IsNotNull(window.Calculate(150, 10, true));
        }

        [TestMethod]
        public void TestMethod_Nalog_False()
        {
            Assert.IsNotNull(window.Calculate(100, 1, false));
        }

        [TestMethod]
        public void TestMethod_Nalog_True()
        {
            Assert.IsNotNull(window.Calculate(100, 1, true));
        }

    }
}
