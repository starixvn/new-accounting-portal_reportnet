namespace Utilities.DateTimes
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static string ToShortFormatedDate(this DateTime? date) => date.HasValue ?
            date.Value.ToShortFormatedDate() :
            string.Empty;

        public static string ToShortFormatedDate(this DateTime date) => $"{date:MMM dd, yyyy}";

        public static string ToShortFormatedDateWithHourMinute(this DateTime? date) => date.HasValue ?
            date.Value.ToShortFormatedDateWithHourMinute() :
            string.Empty;

        public static string ToShortFormatedDateWithHourMinute(this DateTime date) => $"{date:MMM dd, yyyy hh:mm}";

        public static string ToLongFormatedDate(this DateTime? date) => date.HasValue ?
            date.Value.ToLongFormatedDate() :
            string.Empty;

        public static string ToLongFormatedDate(this DateTime date) => $"{date:dd MMM, yyyy hh:mm:ss tt}";

        public static string ToMonthAndDay(this DateTime? date) => date.HasValue ?
            date.Value.ToMonthAndDay() :
            string.Empty;

        public static string ToMonthAndDay(this DateTime date) => $"{date:dd-MMM}";

        public static string ToMonthAndDayWithNoHyphen(this DateTime? date) => date.HasValue ?
            date.Value.ToMonthAndDayWithNoHyphen() :
            string.Empty;

        public static string ToMonthAndDayWithNoHyphen(this DateTime date) => $"{date:dd MMM}";

        public static string ToMonthYear(this DateTime? date) => date.HasValue ?
            date.Value.ToMonthYear() :
            string.Empty;

        public static string ToMonthYear(this DateTime date) => $"{date:MMM yyyy}";

        public static string ToShortFormatedDateWithSlash(this DateTime? date) => date.HasValue ?
            date.Value.ToShortFormatedDateWithSlash() :
            string.Empty;

        public static string ToLongFormatedDateWithSlash(this DateTime date) => $"{date:MM/dd/yyyy hh:mm:ss tt}";

        public static string ToShortFormatedDateWithSlash(this DateTime date) => $"{date:MM/dd/yyyy}";

        public static string ToMonthYearWithSlash(this DateTime? date) => date.HasValue ?
            date.Value.ToMonthYearWithSlash() :
            string.Empty;

        public static string ToMonthYearWithSlash(this DateTime date) => $"{date:MM/yyyy}";

        public static string GetFullMonthName(int month) => $"{new DateTime(1, month, 1):MMMM}";

        public static DateTime FirstDateOfWeek(int year, int week)
        {
            DateTime firstDateOfYear = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - firstDateOfYear.DayOfWeek;
            DateTime firstMonday = firstDateOfYear.AddDays(daysOffset);
            return firstMonday.AddDays((week - 1) * 7);
        }

        public static DateTime LastDateOfWeek(int year, int week)
        {
            DateTime firstDateOfYear = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - firstDateOfYear.DayOfWeek;
            DateTime firstMonday = firstDateOfYear.AddDays(daysOffset);
            return firstMonday.AddDays((week * 7) - 1);
        }

        public static DateTime GetEndOfDate(this DateTime date)
        {
            date = date.AddDays(1).AddSeconds(-1);
            return date;
        }

        public static string ToLongMMMddFormatedDate(this DateTime date) => $"{date:MMM dd, yyyy hh:mm:ss tt}";
    }
}
