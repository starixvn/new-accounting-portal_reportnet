using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using TraderPerformance.Model;
using Utilities.Configurations;

namespace TraderPerformance.Services;

public interface TraderPerformanceOtherSportsExcelService
{
    Task<ExcelPackage> ExportExcelOtherSports(TraderPerformanceOtherSportsModel data, string title);

    Task<ExcelPackage> ExportExcelOtherSportsDetail(TraderPerformanceOtherSportsDetailModel data, string title);
}

public class TraderPerformanceOtherSportsExcelServiceImp(ConfigurationService configurationService) : TraderPerformanceOtherSportsExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet subTotalSheet;

    private const int StartRowIndex = 6;

    private const string GrandTotalText = "Grand Total";

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelOtherSports(TraderPerformanceOtherSportsModel data, string title)
    {
        LoadTemplate("TraderPerformance_OtherSports.xlsx", false);
        BuildTitle(title, "A1:BA2");
        var startIndexOfTotal = data.ReportRows.Count + StartRowIndex;
        BuildBodyRows(data.ReportRows, StartRowIndex);
        BuildTotalRow(data.GrandTotal, startIndexOfTotal);
        ClearTemplate(false);
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelOtherSportsDetail(TraderPerformanceOtherSportsDetailModel data, string title)
    {
        LoadTemplate("TraderPerformance_OtherSports_Detail.xlsx", true);
        BuildTitle(title, "A1:BA2");
        BuildDetailBodyRows(data.ReportRows, data.GrandTotal, StartRowIndex);
        ClearTemplate(true);
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel, bool isDetail)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TraderPerformanceOtherSports/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        if (isDetail)
        {
            subTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
            sheet = ExcelPackage.Workbook.Worksheets["TraderPerformance_OtherSports_D"];
        }
        else
        {
            sheet = ExcelPackage.Workbook.Worksheets["TraderPerformance_OtherSports"];
        }
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IEnumerable<TraderPerformanceOtherSportsModel> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        foreach (var item in list)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            var excelModel = CreateExcelModel(item);
            excelModel.No = index++;
            sheet.ApplyModel(template, excelModel, rowIndex++);
        }
    }

    private void BuildDetailBodyRows(IEnumerable<TraderPerformanceOtherSportsDetailModel> list, TraderPerformanceOtherSportsDetailModel grandTotal, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        foreach (var item in list)
        {
            var template = index % 2 == 0 ? evenSheet : oddSheet;
            foreach (var excelModel in item.ReportRows.Select(CreateDetailExcelModel))
            {
                sheet.ApplyModel(template, excelModel, rowIndex++);
            }
            BuildDetailTotalRow(item.GrandTotal, rowIndex++, item.Name, subTotalSheet);
            var firstRangeOfCell = $"A{rowIndex - item.ReportRows.Count - 1}:A{rowIndex - 2}";
            sheet.Cells[firstRangeOfCell].Merge = true;
            sheet.Cells[firstRangeOfCell].Value = index++;
            var secondRangeOfCell = $"B{rowIndex - item.ReportRows.Count - 1}:B{rowIndex - 2}";
            sheet.Cells[secondRangeOfCell].Merge = true;
            sheet.Cells[secondRangeOfCell].Value = item.Name;
        }
        BuildDetailTotalRow(grandTotal, rowIndex, GrandTotalText, totalSheet);
    }

    private void BuildDetailTotalRow(TraderPerformanceOtherSportsDetailModel subTotal, int indexRow, string text, ExcelWorksheet template)
    {
        var excelModel = CreateDetailExcelModel(subTotal);
        sheet.ApplyModel(template, excelModel, indexRow);
        sheet.Cells[$"A{indexRow}:F{indexRow}"].Merge = true;
        sheet.Cells[$"A{indexRow}:F{indexRow}"].Value = text;
    }

    private static TraderPerformanceOtherSportsExcelModel CreateExcelModel(TraderPerformanceOtherSportsModel model)
    {
        var excelModel = new TraderPerformanceOtherSportsExcelModel
        {
            Name = model.Name,
            MatchNumber = model.MatchNumber,
            Major = model.Major == 0 ? null : model.Major,
            Minor1 = model.Minor1 == 0 ? null : model.Minor1,
            Minor2 = model.Minor2 == 0 ? null : model.Minor2,
            Minor3 = model.Minor3 == 0 ? null : model.Minor3,
            Others = model.Others
        };
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[0], "WinLoss");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[1], "TurnOver");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[2], "LiveBetCount");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[3], "Margin");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[4], "Commission");
        return excelModel;
    }

    private static TraderPerformanceOtherSportsDetailExcelModel CreateDetailExcelModel(TraderPerformanceOtherSportsDetailModel model)
    {
        var excelModel = new TraderPerformanceOtherSportsDetailExcelModel
        {
            EventDate = model.EventDate,
            LeagueName = model.LeagueName,
            Match = $"{model.HomeName}-{model.AwayName}",
            Maxbet = model.Maxbet,
            HaftTime = $"{model.HTHomeScore}-{model.HTAwayScore}",
            FullTime = $"{model.FTHomeScore}-{model.FTAwayScore}"
        };
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[0], "WinLoss");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[1], "TurnOver");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[2], "LiveBetCount");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[3], "Margin");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[4], "Commission");
        return excelModel;
    }

    private static void UpdateChildInfoExcelModel<TModel, TData>(TModel model, TData data, string category)
    {
        var type = model.GetType();
        var dataType = data.GetType();
        var isPercent = category == "Margin";
        type.GetProperty($"{category}FirstHalfOneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfOneXTwo")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}OneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("OneXTwo")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}FirstHalfHdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfHdp")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}Hdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("Hdp")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}FirstHalfOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfOU")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}OU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("OU")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}FirstHalfOE")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfOE")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}OE")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("OE")?.GetValue(data)), isPercent));
        type.GetProperty($"{category}Overall")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("Overall")?.GetValue(data)), isPercent));
    }

    private static decimal? FormatDecimalData(decimal data, bool isPercent)
    {
        if (isPercent)
        {
            return data == 0 ? null : data / 100;
        }

        return data == 0 ? null : data;
    }

    private void BuildTotalRow(TraderPerformanceOtherSportsModel grandTotal, int indexRow)
    {
        var excelModel = CreateExcelModel(grandTotal);
        sheet.ApplyModel(totalSheet, excelModel, indexRow);
        sheet.Cells[$"A{indexRow}:B{indexRow}"].Merge = true;
        sheet.Cells[$"A{indexRow}:B{indexRow}"].Value = GrandTotalText;
    }

    private void ClearTemplate(bool isDetail)
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        if (isDetail)
        {
            ExcelPackage.Workbook.Worksheets.Delete("SubTotalTemplate");
        }
    }
}