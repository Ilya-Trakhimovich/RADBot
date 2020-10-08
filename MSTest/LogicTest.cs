using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RADParse.Infrastructure.Defects;

namespace MSTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void LogicForAddDefects_ReturnCorrectValue()
        {
            var logic = new LogicForAddDefects();

            var expected = new List<int>() { 21 };

            var result = logic.GetSections(23);

            Assert.AreEqual(expected, result);
        }
    }
}
