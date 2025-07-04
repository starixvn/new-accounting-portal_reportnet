using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using SoccerPerformanceDetail.Model;

namespace SoccerPerformanceDetail.Services;

public static class CalendarHelper
{
    public static List<DateTime> GetWeekDates(int year, int weekIndex)
    {
        var result = new List<DateTime>();

        for (var month = 1; month <= 12; month++)
        {
            result.AddRange(GetWeekDates(year, month, weekIndex));
        }

        return result;
    }

    public static List<DateTime> GetWeekDates(int year, int month, int weekIndex)
    {
        var result = new List<DateTime>();

        var firstDayOfMonth = new DateTime(year, month, 1);
        var daysOffset = ((int)DayOfWeek.Monday - (int)firstDayOfMonth.DayOfWeek + 7) % 7;
        var firstMonday = firstDayOfMonth.AddDays(daysOffset);

        var weekStart = firstMonday == firstDayOfMonth ? firstMonday : firstMonday.AddDays(-7);
        weekStart = weekStart.AddDays((weekIndex - 1) * 7);
        var weekEnd = weekStart.AddDays(7);

        // Only get the date in the same month
        for (var date = weekStart; date < weekEnd; date = date.AddDays(1))
        {
            if (date.Month == month)
            {
                result.Add(date);
            }
        }

        return result;
    }

    public static List<DateTime> GetWeekDatesOfYear(int year, int week)
    {
        var result = new List<DateTime>();

        var firstDayOfYear = new DateTime(year, 1, 1);
        var daysOffset = ((int)DayOfWeek.Monday - (int)firstDayOfYear.DayOfWeek + 7) % 7;
        var firstMonday = firstDayOfYear.AddDays(daysOffset);

        var weekStart = firstMonday == firstDayOfYear ? firstMonday : firstMonday.AddDays(-7);
        weekStart = weekStart.AddDays((week - 1) * 7);
        var weekEnd = weekStart.AddDays(7);

        // Only get the date in the same month
        for (var date = weekStart; date < weekEnd; date = date.AddDays(1))
        {
            result.Add(date);
        }

        return result;
    }

}

public class TraderMonthlyWeekMainSheetExcelService(
    ExcelWorksheet template,
    ExcelWorksheet sheet,
    int year,
    List<int> months,
    List<TraderPerformanceWeekRawData> rawData)
{
    private int rowIndex = 3;
    private int numberIndex = 1;

    public void Build()
    {
        CreateMainSheetHeader();
        CreateMainSheetBody();
        BuildTotalRow();
    }

    private void CreateMainSheetHeader()
    {
        var monthCount = months.Count;

        var startPasteColumn = 7; // Column G

        for (var i = 0; i < monthCount; i++)
        {
            // Calculate paste position (shift 5 columns each time)
            var currentColumn = startPasteColumn + (i * 5) + 1;

            // Create destination range (same number of rows and columns)
            var destRange = sheet.Cells[
                1, currentColumn,         // Start row, start column
                2, currentColumn + 4];     // End row, end column (H:L = 5 columns)

            template.Cells["H1:L2"].Copy(destRange);

            var currentMonth = new DateTime(year, months[i], 1);
            sheet.Cells[1, currentColumn].Value = currentMonth.ToString("MMMM - yyyy");
        }
    }

    private void CreateMainSheetBody()
    {
        for (var i = 1; i <= 6; i++)
        {
            CreateMainSheetBodyWeek(i);
        }
    }

    private void CreateMainSheetBodyWeek(int week)
    {
        var columnCount = 7 + months.Count * 5;

        // Copy Week header
        template.Cells[3, 1, 3, 7].Copy(sheet.Cells[rowIndex, 1, rowIndex, 7]);
        for (var i = 0; i < months.Count; i++)
        {
            template.Cells[3, 8, 3, 12].Copy(sheet.Cells[rowIndex, 8 + 5 * i, rowIndex, 12 + 5 * i]);
            sheet.Columns[8 + 5 * i, 12 + 5 * i].Width = 12;
        }

        sheet.Cells[rowIndex, 1, rowIndex, columnCount].Merge = true;
        sheet.Cells[rowIndex, 1].Value = $"Week {week}";

        // Week body
        var weekDates = CalendarHelper.GetWeekDates(year, week);
        var weekData = rawData.Where(x => weekDates.Contains(x.EventDate)).ToList();
        var rowGroups = weekData.GroupBy(x => new { x.TraderName, x.LeagueName, x.Grade, x.TeamName, x.Department });
        rowIndex++;

        foreach (var row in rowGroups)
        {
            var key = row.Key;
            var rowData = row.ToList();

            var trader = key.TraderName;
            var grade = key.Grade;
            var teamName = key.TeamName;
            var department = key.Department;
            var league = key.LeagueName;

            var allWeekDates = months.Select(m => CalendarHelper.GetWeekDates(year, m, week)).SelectMany(d => d);
            rowData = rowData.Where(x => allWeekDates.Contains(x.EventDate)).ToList();
            if (rowData.Count == 0)
            {
                continue;
            }

            BuildWeekTitle(trader, grade, teamName, department, league);
            BuildWeekStatistic(week, rowData, trader, teamName, grade, department, league);

            numberIndex++;
            rowIndex++;
        }
    }

    private void BuildWeekTitle(string trader, string grade, string teamName, string department, string league)
    {
        template.Cells[4, 1, 4, 7].Copy(sheet.Cells[rowIndex, 1, rowIndex, 7]);
        sheet.Cells[rowIndex, 1].Value = numberIndex;
        sheet.Cells[rowIndex, 2].Value = trader;
        sheet.Cells[rowIndex, 3].Value = grade;
        sheet.Cells[rowIndex, 4].Value = teamName;
        sheet.Cells[rowIndex, 5].Value = department;
        sheet.Cells[rowIndex, 6].Value = year;
        sheet.Cells[rowIndex, 7].Value = league;
    }

    private void BuildWeekStatistic(
        int week,
        List<TraderPerformanceWeekRawData> rowData,
        string trader,
        string teamName,
        string grade,
        string department,
        string league)
    {
        var columnIndex = 7;

        foreach (var month in months)
        {
            var monthDates = CalendarHelper.GetWeekDates(year, month, week);
            var traderDataForMonth = GetTraderDataForMonth(rowData, monthDates, trader, teamName, grade, department, league);

            template.Cells[4, 8, 4, 12].Copy(sheet.Cells[rowIndex, columnIndex + 1, rowIndex, columnIndex + 5]);
            if (traderDataForMonth.Count == 0)
            {
                AssignEmptyCell(rowIndex, columnIndex + 1, columnIndex + 5);
            }
            else
            {
                AssignWeekStatisticForMonth(traderDataForMonth, columnIndex);
            }

            columnIndex += 5;
        }
    }

    private void AssignWeekStatisticForMonth(List<TraderPerformanceWeekRawData> data, int columnIndex)
    {
        var turnover = data.Sum(x => x.Turnover);
        var netWinLoss = data.Sum(x => x.NetWinLoss);
        var matchCount = data.Sum(x => x.MatchCount);
        var betCount = data.Sum(x => x.BetCount);

        sheet.Cells[rowIndex, columnIndex + 1].Value = matchCount;
        sheet.Cells[rowIndex, columnIndex + 2].Value = turnover;
        sheet.Cells[rowIndex, columnIndex + 3].Value = netWinLoss;
        sheet.Cells[rowIndex, columnIndex + 5].Value = betCount;
        sheet.Cells[rowIndex, columnIndex + 4].Value = netWinLoss / turnover;
    }

    private void AssignEmptyCell(int row, int columnFrom, int columnTo)
    {
        for (var i = columnFrom; i <= columnTo; i++)
        {
            sheet.Cells[row, i].Value = "-";
        }
    }

    private static List<TraderPerformanceWeekRawData> GetTraderDataForMonth(
        List<TraderPerformanceWeekRawData> rowData,
        List<DateTime> monthDates,
        string trader,
        string teamName,
        string grade,
        string department,
        string league)
    {
        return rowData.Where(x => monthDates.Contains(x.EventDate)
                                  && x.TraderName == trader
                                  && x.TeamName == teamName
                                  && x.Grade == grade
                                  && x.Department == department
                                  && x.LeagueName == league)
            .ToList();
    }

    private void BuildTotalRow()
    {
        template.Cells["A5:G5"].Copy(sheet.Cells[rowIndex, 1, rowIndex, 5]);

        var columnIndex = 7;
        foreach (var month in months)
        {
            var destRange = sheet.Cells[
                rowIndex, columnIndex + 1,
                rowIndex, columnIndex + 5];

            template.Cells["H5:L5"].Copy(destRange);

            var monthData = rawData.Where(x => x.EventDate.Month == month && x.EventDate.Year == year).ToList();
            if (monthData.Count == 0)
            {
                AssignEmptyCell(rowIndex, columnIndex + 1, columnIndex + 5);
            }
            else
            {
                AssignWeekStatisticForMonth(monthData, columnIndex);
            }

            columnIndex += 5;
        }
    }
}

public class TraderMonthlyWeekMonthCoverageSheetExcelService(
    ExcelWorksheet template,
    ExcelWorksheet sheet,
    int year,
    List<int> months,
    List<TraderPerformanceWeekRawData> rawData)
{
    private int rowIndex = 3;

    public void Build()
    {
        foreach (var month in months)
        {
            template.Cells[3, 1, 3, 7].Copy(sheet.Cells[rowIndex, 1, rowIndex, 7]);
            var monthName = new DateTime(2000, month, 1).ToString("MMMM");
            sheet.Cells[rowIndex, 1].Value = monthName;

            var rawDataForMonth = rawData.Where(x => x.EventDate.Month == month && x.EventDate.Year == year).ToList();
            if (rawDataForMonth.Count == 0)
            {
                AssignEmptyCell(rowIndex, 2, 7);
            }
            else
            {
                AssignMonthStatistic(rawDataForMonth);
            }

            rowIndex++;
        }
    }

    private void AssignMonthStatistic(List<TraderPerformanceWeekRawData> rawDataForMonth)
    {
        var cphMatchCount = rawDataForMonth.Where(x => x.Department == "CPH").Sum(x => x.MatchCount);
        var sphMatchCount = rawDataForMonth.Where(x => x.Department == "SPH").Sum(x => x.MatchCount);
        var lgMatchCount = rawDataForMonth.Where(x => x.Department == "LG").Sum(x => x.MatchCount);
        var totalMatchCount = cphMatchCount + sphMatchCount + lgMatchCount;

        sheet.Cells[rowIndex, 2].Value = cphMatchCount;
        sheet.Cells[rowIndex, 3].Value = sphMatchCount;
        sheet.Cells[rowIndex, 4].Value = lgMatchCount;
        sheet.Cells[rowIndex, 5].Value = totalMatchCount;
        sheet.Cells[rowIndex, 6].Value = (cphMatchCount + (decimal)sphMatchCount) / totalMatchCount;
        sheet.Cells[rowIndex, 7].Value = (decimal)lgMatchCount / totalMatchCount;
    }

    private void AssignEmptyCell(int row, int columnFrom, int columnTo)
    {
        for (var i = columnFrom; i <= columnTo; i++)
        {
            sheet.Cells[row, i].Value = "-";
        }
    }
}

public class TraderMonthlyWeekWeekCoverageSheetExcelService(
    ExcelWorksheet template,
    ExcelWorksheet sheet,
    int year,
    List<int> weeks,
    List<TraderPerformanceWeekRawData> rawData)
{
    private int rowIndex = 3;

    public void Build()
    {
        foreach (var week in weeks)
        {
            // Copy Week header
            BuildWeekHeader(week);

            // Week body
            BuildWeekBody(week);
        }
    }

    private void BuildWeekHeader(int week)
    {
        var datesOfWeek = CalendarHelper.GetWeekDatesOfYear(year, week);
        var rawDataForWeek = rawData.Where(x => datesOfWeek.Contains(x.EventDate)).ToList();

        template.Cells[3, 1, 3, 7].Copy(sheet.Cells[rowIndex, 1, rowIndex, 7]);
        sheet.Cells[rowIndex, 1].Value = $"Week {week}";
        if (rawDataForWeek.Count == 0)
        {
            AssignEmptyCell(rowIndex, 2, 7);
        }
        else
        {
            AssignDayStatistic(rawDataForWeek);
        }

        rowIndex++;
    }

    private void BuildWeekBody(int week)
    {
        var datesOfWeek = CalendarHelper.GetWeekDatesOfYear(year, week);
        foreach (var date in datesOfWeek)
        {
            var rawDataForDate = rawData.Where(x => x.EventDate == date).ToList();

            template.Cells[4, 1, 4, 7].Copy(sheet.Cells[rowIndex, 1, rowIndex, 7]);
            sheet.Cells[rowIndex, 1].Value = date;

            if (rawDataForDate.Count == 0)
            {
                AssignEmptyCell(rowIndex, 2, 7);
            }
            else
            {
                AssignDayStatistic(rawDataForDate);
            }

            rowIndex++;
        }
    }

    private void AssignDayStatistic(List<TraderPerformanceWeekRawData> data)
    {
        var cphMatchCount = data.Where(x => x.Department == "CPH").Sum(x => x.MatchCount);
        var sphMatchCount = data.Where(x => x.Department == "SPH").Sum(x => x.MatchCount);
        var lgMatchCount = data.Where(x => x.Department == "LG").Sum(x => x.MatchCount);
        var totalMatchCount = cphMatchCount + sphMatchCount + lgMatchCount;

        sheet.Cells[rowIndex, 2].Value = cphMatchCount;
        sheet.Cells[rowIndex, 3].Value = sphMatchCount;
        sheet.Cells[rowIndex, 4].Value = lgMatchCount;
        sheet.Cells[rowIndex, 5].Value = totalMatchCount;
        sheet.Cells[rowIndex, 6].Value = (cphMatchCount + (decimal)sphMatchCount) / totalMatchCount;
        sheet.Cells[rowIndex, 7].Value = (decimal)lgMatchCount / totalMatchCount;
    }

    private void AssignEmptyCell(int row, int columnFrom, int columnTo)
    {
        for (var i = columnFrom; i <= columnTo; i++)
        {
            sheet.Cells[row, i].Value = "-";
        }
    }
}


public class TraderMonthlyWeekReportService : IDisposable
{
    private readonly ExcelPackage excelPackage;

    private readonly TraderMonthlyWeekMainSheetExcelService mainSheetService;
    private readonly TraderMonthlyWeekMonthCoverageSheetExcelService monthCoverageSheetService;
    private readonly TraderMonthlyWeekWeekCoverageSheetExcelService weekCoverageSheetService;

    public TraderMonthlyWeekReportService(string templatePath, int year, List<int> months, List<int> weeks, List<TraderPerformanceWeekRawData> rawData)
    {
        months = months.Distinct().OrderBy(x => x).ToList();
        weeks = weeks.Distinct().OrderBy(x => x).ToList();
        excelPackage = new ExcelPackage(new FileInfo(templatePath));
        var mainTemplate = excelPackage.Workbook.Worksheets["Sheet1"];
        var mainSheet = excelPackage.Workbook.Worksheets.Add("Trader Monthly Performance", mainTemplate);
        var monthCoverageTemplate = excelPackage.Workbook.Worksheets["Sheet2"];
        var monthCoverageSheet = excelPackage.Workbook.Worksheets.Add("Saba Match Coverage-Month", monthCoverageTemplate);
        var weekCoverageTemplate = excelPackage.Workbook.Worksheets["Sheet3"];
        var weekCoverageSheet = excelPackage.Workbook.Worksheets.Add("Saba Match Coverage-Week", weekCoverageTemplate);

        mainSheetService = new TraderMonthlyWeekMainSheetExcelService(mainTemplate, mainSheet, year, months, rawData);
        monthCoverageSheetService = new TraderMonthlyWeekMonthCoverageSheetExcelService(monthCoverageTemplate, monthCoverageSheet, year, months, rawData);
        weekCoverageSheetService = new TraderMonthlyWeekWeekCoverageSheetExcelService(weekCoverageTemplate, weekCoverageSheet, year, weeks, rawData);
    }

    public MemoryStream ExportExcel()
    {
        mainSheetService.Build();
        monthCoverageSheetService.Build();
        weekCoverageSheetService.Build();

        DeleteTemplateSheet();
        return ToMemoryStream();
    }

    private MemoryStream ToMemoryStream()
    {
        var memoryStream = new MemoryStream();
        excelPackage.SaveAs(memoryStream);
        memoryStream.Position = 0;
        excelPackage.Dispose();

        return memoryStream;
    }

    private void DeleteTemplateSheet()
    {
        excelPackage.Workbook.Worksheets.Delete("Sheet1");
        excelPackage.Workbook.Worksheets.Delete("Sheet2");
        excelPackage.Workbook.Worksheets.Delete("Sheet3");
    }

    public void Dispose()
    {
        excelPackage?.Dispose();
    }
}