using AutoMapper;
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

public class TraderPerformanceOtherSportsExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : TraderPerformanceOtherSportsExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 6;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelOtherSports(TraderPerformanceOtherSportsModel data, string title)
    {
        LoadTemplate("TraderPerformance_OtherSports.xlsx");
        BuildTitle(title, "A1:BA2");
        var startIndexOfTotal = data.ReportRows.Count + StartRowIndex;
        BuildBodyRows(data.ReportRows, StartRowIndex);
        BuildTotalRow(data.GrandTotal, startIndexOfTotal);
        ClearTemplate(false);
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelOtherSportsDetail(TraderPerformanceOtherSportsDetailModel data, string title)
    {
        LoadTemplate("TraderPerformance_OtherSports_Detail.xlsx");
        BuildTitle(title, "A1:BA2");
        var startIndexOfTotal = data.ReportRows.Count + StartRowIndex;
        //BuildBodyRows(data.ReportRows, StartRowIndex);
        //BuildTotalRow(data.GrandTotal, startIndexOfTotal);
        ClearTemplate(true);
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TraderPerformanceOtherSports/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["TraderPerformance_OtherSports"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
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

    private static TraderPerformanceOtherSportsExcelModel CreateExcelModel(TraderPerformanceOtherSportsModel model)
    {
        var excelModel = new TraderPerformanceOtherSportsExcelModel
        {
            Name = model.Name,
            MatchNumber = model.MatchNumber,
            Major = model.Major,
            Minor1 = model.Minor1,
            Minor2 = model.Minor2,
            Minor3 = model.Minor3,
            Others = model.Others
        };
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[0], "WinLoss");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[1], "TurnOver");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[2], "LiveBetCount");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[3], "Margin");
        UpdateChildInfoExcelModel(excelModel, model.ChildInfo[4], "Commission");
        return excelModel;
    }

    private static void UpdateChildInfoExcelModel(TraderPerformanceOtherSportsExcelModel model, TraderPerformanceOtherSportsModel data, string category)
    {
        var type = model.GetType();
        type.GetProperty($"{category}FirstHalfOneXTwo")?.SetValue(model, FormatDecimalData(data.FirstHalfOneXTwo));
        type.GetProperty($"{category}OneXTwo")?.SetValue(model, FormatDecimalData(data.OneXTwo));
        type.GetProperty($"{category}FirstHalfHdp")?.SetValue(model, FormatDecimalData(data.FirstHalfHdp));
        type.GetProperty($"{category}Hdp")?.SetValue(model, FormatDecimalData(data.Hdp));
        type.GetProperty($"{category}FirstHalfOU")?.SetValue(model, FormatDecimalData(data.FirstHalfOU));
        type.GetProperty($"{category}OU")?.SetValue(model, FormatDecimalData(data.OU));
        type.GetProperty($"{category}FirstHalfOE")?.SetValue(model, FormatDecimalData(data.FirstHalfOE));
        type.GetProperty($"{category}OE")?.SetValue(model, FormatDecimalData(data.OE));
        type.GetProperty($"{category}Overall")?.SetValue(model, FormatDecimalData(data.Overall));
    }

    private static decimal? FormatDecimalData(decimal data)
    {
        return data == 0 ? null : data;
    }

    private void BuildTotalRow(TraderPerformanceOtherSportsModel grandTotal, int indexRow)
    {
        var excelModel = CreateExcelModel(grandTotal);
        sheet.ApplyModel(totalSheet, excelModel, indexRow);
        sheet.Cells[$"A{indexRow}:B{indexRow}"].Merge = true;
        sheet.Cells[$"A{indexRow}:B{indexRow}"].Value = "Grand Total";
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