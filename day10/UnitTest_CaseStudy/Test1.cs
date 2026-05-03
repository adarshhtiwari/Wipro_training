using CalculatorApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CalculatorTests
{
    [TestClass]
    public class Calculatortests
    {
        private Calculator calc;

        [TestInitialize]
        public void Setup()
        {
            calc = new Calculator();
        }

        [TestMethod]
        public void Test_Add()
        {
            double result = calc.Add(5, 3);
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Test_Subtract()
        {
            double result = calc.Subtract(10, 4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_Multiply()
        {
            double result = calc.Multiply(2, 3);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_Divide()
        {
            double result = calc.Divide(10, 2);
            Assert.AreEqual(5, result);
        }

        //[TestMethod]
        //public void Test_Divide_ByZero()
        //{
        //    Assert.ThrowsException<DivideByZeroException>(() => calc.Divide(10, 0));
        //}

        [TestMethod]
        public void Test_Add_Zero()
        {
            double result = calc.Add(5, 0);
            Assert.AreEqual(5, result);
        }
    }
}