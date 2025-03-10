namespace SMAWinLoss.Summary;

using OfficeOpenXml;
using Utilities.Configurations;

public interface ExcelBuilder
{
    ExcelPackage Build(int year, string title, WinLossModel winlosses);
}

public class ExcelBuilderImp(ConfigurationService configurationService) : ExcelBuilder
{
    private ExcelWorksheet summarySheet;

    private ExcelWorksheet summaryEvenSheet;

    private ExcelWorksheet summaryOddSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 4;

    private const int StartColIndex = 1;

    private const int EndColIndex = 18;

    private const int NumberRowPerUser = 5;

    private const int StartMonthlyColIndex = 5;

    private ExcelPackage ExcelPackage { get; set; }

    public ExcelPackage Build(int year, string title, WinLossModel winlosses)
    {
        LoadTemplate();
        BuildTitle(title);
        BuildHeader(year);
        BuildBody(winlosses);
        BuildTotal(winlosses);
        FormatWorksheets();
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate()
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SMAWinLoss/Summary.xlsx";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        summarySheet = ExcelPackage.Workbook.Worksheets.Copy("SummaryOdd", "SMA Win Loss Summary");
        summaryEvenSheet = ExcelPackage.Workbook.Worksheets["SummaryEven"];
        summaryOddSheet = ExcelPackage.Workbook.Worksheets["SummaryOdd"];
        totalSheet = ExcelPackage.Workbook.Worksheets["Total"];
    }

    private void BuildTitle(string title)
    {
        summarySheet.Cells["A1:R1"].Value = title;
    }

    private void BuildHeader(int year)
    {
        summarySheet.Cells["E3:E3"].Value = $"Jan, {year}";
        summarySheet.Cells["F3:F3"].Value = $"Feb, {year}";
        summarySheet.Cells["G3:G3"].Value = $"Mar, {year}";
        summarySheet.Cells["H3:H3"].Value = $"Apr, {year}";
        summarySheet.Cells["I3:I3"].Value = $"May, {year}";
        summarySheet.Cells["J3:J3"].Value = $"Jun, {year}";
        summarySheet.Cells["K3:K3"].Value = $"Jul, {year}";
        summarySheet.Cells["L3:L3"].Value = $"Aug, {year}";
        summarySheet.Cells["M3:M3"].Value = $"Sep, {year}";
        summarySheet.Cells["N3:N3"].Value = $"Oct, {year}";
        summarySheet.Cells["O3:O3"].Value = $"Nov, {year}";
        summarySheet.Cells["P3:P3"].Value = $"Dec, {year}";
    }

    private void BuildBody(WinLossModel winlosses)
    {
        var rowIndex = StartRowIndex;
        foreach (var item in winlosses.UsernameWinLosses)
        {
            BuildRow(item, rowIndex);
            rowIndex++;
        }
    }

    private void BuildTotal(WinLossModel winlosses)
    {
        var numberRowOfTotal = 0;
        var rowIndex = StartRowIndex + winlosses.UsernameWinLosses.Count;
        foreach (var item in winlosses.Total)
        {
            BuildTotalRow(item, rowIndex);
            rowIndex++;
            numberRowOfTotal++;
        }

        summarySheet.Cells[$"A{rowIndex - numberRowOfTotal}:C{rowIndex - 1}"].Merge = true;
    }

    private void BuildRow(UsernameWinLoss username, int rowIndex)
    {
        var userIndex = Convert.ToInt32((rowIndex - StartRowIndex) / NumberRowPerUser);
        if (userIndex % 2 == 0)
        {
            summaryEvenSheet
                .Cells[StartRowIndex, StartColIndex, StartRowIndex, EndColIndex]
                .Copy(summarySheet.Cells[rowIndex, StartColIndex, rowIndex, EndColIndex]);
        }
        else
        {
            summaryOddSheet
                .Cells[StartRowIndex, StartColIndex, StartRowIndex, EndColIndex]
                .Copy(summarySheet.Cells[rowIndex, StartColIndex, rowIndex, EndColIndex]);
        }

        var monthlyColIndex = StartMonthlyColIndex;
        summarySheet.Cells[$"A{rowIndex}:A{rowIndex}"].Value = username.Username;
        summarySheet.Cells[$"B{rowIndex}:B{rowIndex}"].Value = username.Role;
        summarySheet.Cells[$"C{rowIndex}:C{rowIndex}"].Value = username.Currency;
        summarySheet.Cells[$"D{rowIndex}:D{rowIndex}"].Value = username.Name;

        foreach (var month in username.Months)
        {
            summarySheet.Cells[rowIndex, monthlyColIndex].Value = month.Value;
            monthlyColIndex++;
        }

        summarySheet.Cells[$"R{rowIndex}:R{rowIndex}"].Value = username.VirtualRate;
    }

    private void BuildTotalRow(UsernameWinLoss username, int rowIndex)
    {
        totalSheet
            .Cells[StartRowIndex, StartColIndex, StartRowIndex, EndColIndex]
            .Copy(summarySheet.Cells[rowIndex, StartColIndex, rowIndex, EndColIndex]);
        var monthlyColIndex = StartMonthlyColIndex;
        summarySheet.Cells[$"A{rowIndex}:A{rowIndex}"].Value = username.Username;
        summarySheet.Cells[$"B{rowIndex}:B{rowIndex}"].Value = username.Role;
        summarySheet.Cells[$"C{rowIndex}:C{rowIndex}"].Value = username.Currency;
        summarySheet.Cells[$"D{rowIndex}:D{rowIndex}"].Value = username.Name;

        foreach (var month in username.Months)
        {
            summarySheet.Cells[rowIndex, monthlyColIndex].Value = month.Value;
            monthlyColIndex++;
        }
    }

    private void FormatWorksheets()
    {
        summarySheet.Cells[summarySheet.Dimension.Address].AutoFitColumns();
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("SummaryOdd");
        ExcelPackage.Workbook.Worksheets.Delete("SummaryEven");
        ExcelPackage.Workbook.Worksheets.Delete("Total");
    }
}