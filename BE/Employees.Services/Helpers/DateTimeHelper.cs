namespace Employees.Services.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetMaxDate(DateTime date1, DateTime date2)
            => date1.CompareTo(date2) >= 0 ? date1 : date2;

        public static DateTime GetMinDate(DateTime date1, DateTime date2)
            => date1.CompareTo(date2) <= 0 ? date1 : date2;
    }
}
