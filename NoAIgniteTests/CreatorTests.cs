using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoAIgnite;
using System.Globalization;

namespace NoAIgniteTests
{
    [TestClass]
    public class CreatorTests
    {
        [TestMethod]
        public void DateFormatSameYearTest()
        {
            string[] testArray = { "10.01.2022", "22.02.2022" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10.01 - 22.02.2022", result);
        }

        [TestMethod]
        public void DateFormatSameMonthTest()
        {
            string[] testArray = { "10.01.2022", "22.01.2022" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10 - 22.01.2022", result);
        }

        [TestMethod]
        public void DotSeparationTest()
        {
            string[] testArray = { "10.01.2022", "22.01.2023" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10.01.2022 - 22.01.2023", result);
        }

        [TestMethod]
        public void SlashSeparationTest()
        {
            string[] testArray = { "10/01/2022", "22/01/2023" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10/01/2022 - 22/01/2023", result);
        }

        [TestMethod]
        public void DashSeparationTest()
        {
            string[] testArray = { "10-01-2022", "22-01-2023" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10-01-2022 - 22-01-2023", result);
        }

        [TestMethod]
        public void ComaSeparationTest()
        {
            string[] testArray = { "10,01,2022", "22,01,2023" };

            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("10,01,2022 - 22,01,2023", result);
        }
        
        [TestMethod]
        public void USCultureTest()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            string[] testArray = { "06.21.2022", "07.21.2022" };
            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);
            
            // changes the culture back to avoid errors in other tests
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL");

            var result = creator.Range();

            Assert.AreEqual("06.21 - 07.21.2022", result);
        }

        [TestMethod]
        public void YearOnFirstFieldTest()
        {
            string[] testArray = { "2022.06.01", "2023.06.12" };
            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("2022.06.01 - 2023.06.12", result);
        }

        [TestMethod]
        public void YearOnFirstFieldFormatSameYearTest()
        {
            string[] testArray = { "2022/06/01", "2022/07/12" };
            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("2022/06/01 - 07/12", result);
        }

        [TestMethod]
        public void YearOnFirstFieldFormatSameMonthTest()
        {
            string[] testArray = { "2022-06-01", "2022-06-12" };
            DateRangeCreator creator = new DateRangeCreator(testArray[0], testArray[1]);

            var result = creator.Range();

            Assert.AreEqual("2022-06-01 - 12", result);
        }
    }
}
