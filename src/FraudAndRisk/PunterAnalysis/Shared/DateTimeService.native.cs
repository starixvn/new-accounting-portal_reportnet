namespace PunterAnalysis.Shared;

using System.Globalization;

public interface DateTimeService
{
    DateTime GetFirstDateOfWeekIso8601(int year, int weekOfYear);

    string FormatDateTimeByPeriodType(int periodType, DateTime date);
}

public class DateTimeServiceImp : DateTimeService
{
    private const int DayOffsetFromStartOfWeek = 3;

    public DateTime GetFirstDateOfWeekIso8601(int year, int weekOfYear)
    {
        var january = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var daysOffset = DayOfWeek.Thursday - january.DayOfWeek;

        var firstThursday = january.AddDays(daysOffset);
        var calendar = CultureInfo.CurrentCulture.Calendar;
        var firstWeek = calendar.GetWeekOfYear(firstThursday.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var weekNum = firstWeek <= 1 ? weekOfYear - 1 : weekOfYear;

        var result = firstThursday.AddDays(weekNum * 7);
        return result.AddDays(-DayOffsetFromStartOfWeek).Date;
    }

    public static int GetWeekOfYearIso8601(DateTime dateTime)
    {
        var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dateTime);
        var realDate = day is >= DayOfWeek.Monday and <= DayOfWeek.Wednesday
            ? dateTime.AddDays(DayOffsetFromStartOfWeek)
            : dateTime;

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(realDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public string FormatDateTimeByPeriodType(int periodType, DateTime date)
    {
        return (PeriodType)periodType switch
        {
            PeriodType.Daily => $"{date:yyyy-MM-dd}",
            PeriodType.Weekly => $"{date:MM/yyyy} - Week {GetWeekOfYearIso8601(date)}",
            _ => $"{date:MM/yyyy}"
        };
    }
}