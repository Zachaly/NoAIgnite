using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoAIgnite;

namespace NoAIgniteTests
{
    [TestClass]
    public class ErrorTests
    {
        [TestMethod]
        public void NullArrayTest()
        {
            string[] testArray = null;

            var result = Program.CreateMessage(testArray);

            Assert.AreEqual(Program.ErrorMessage, result);
        }

        [TestMethod]
        public void TooShortArrayTest()
        {
            string[] testArray = { "10.1.2022" };

            var result = Program.CreateMessage(testArray);

            Assert.AreEqual(Program.ErrorMessage, result);
        }

        [TestMethod]
        public void InvalidInputDataTest()
        {
            string[] testArray = { "jan", "pawel" };

            var result = Program.CreateMessage(testArray);

            Assert.AreEqual(Program.ErrorMessage, result);
        }

        [TestMethod]
        public void StartDateBiggerTest()
        {
            string[] testArray = { "10.1.2022", "10.01.2021" };

            var result = Program.CreateMessage(testArray);

            Assert.AreEqual(Program.ErrorMessage, result);
        }
    }
}
