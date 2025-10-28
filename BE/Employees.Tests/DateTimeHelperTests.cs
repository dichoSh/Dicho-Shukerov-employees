using Employees.Services.Helpers;

namespace Employees.Tests
{
    public class DateTimeHelperTests
    {
        [Test]
        public void GetMaxDate_WhenFirstIsGreater_ReturnsFirstDate()
        {
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddDays(-1);

            var maxDate = DateTimeHelper.GetMaxDate(date1, date2);

            Assert.That(maxDate, Is.EqualTo(date1));
        }

        [Test]
        public void GetMaxDate_WhenSecondIsGreater_ReturnsSecondDate()
        {
            var date1 = DateTime.Now.AddDays(-1);
            var date2 = DateTime.Now;

            var maxDate = DateTimeHelper.GetMaxDate(date1, date2);

            Assert.That(maxDate, Is.EqualTo(date2));
        }

        [Test]
        public void GetMaxDate_WhenEquals_ReturnsDate()
        {
            var date1 = DateTime.Now;

            var maxDate = DateTimeHelper.GetMaxDate(date1, date1);

            Assert.That(maxDate, Is.EqualTo(date1));
        }

        [Test]
        public void GetMinDate_WhenFirstIsGreater_ReturnsSecondDate()
        {
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddDays(-1);

            var maxDate = DateTimeHelper.GetMinDate(date1, date2);

            Assert.That(maxDate, Is.EqualTo(date2));
        }

        [Test]
        public void GetMinDate_WhenSecondIsGreater_ReturnsFirstDate()
        {
            var date1 = DateTime.Now.AddDays(-1);
            var date2 = DateTime.Now;

            var maxDate = DateTimeHelper.GetMinDate(date1, date2);

            Assert.That(maxDate, Is.EqualTo(date1));
        }

        [Test]
        public void GetMinDate_WhenEquals_ReturnsDate()
        {
            var date1 = DateTime.Now;

            var maxDate = DateTimeHelper.GetMaxDate(date1, date1);

            Assert.That(maxDate, Is.EqualTo(date1));
        }
    }
}
