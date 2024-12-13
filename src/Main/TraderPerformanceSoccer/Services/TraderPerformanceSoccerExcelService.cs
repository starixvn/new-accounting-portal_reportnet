using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using TraderPerformance.Model;
using Utilities.Configurations;

namespace TraderPerformance.Services;

public interface TraderPerformanceSoccerExcelService
{
    Task<ExcelPackage> ExportExcel(TraderPerformanceSoccerModel data, string title);

    Task<ExcelPackage> ExportExcelForDetail(TraderPerformanceSoccerDetailModel data, string title);
}

public class TraderPerformanceSoccerExcelServiceImp(ConfigurationService configurationService) : TraderPerformanceSoccerExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet subTotalSheet;

    private const int StartRowIndex = 6;

    private const string GrandTotalText = "Grand Total";

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(TraderPerformanceSoccerModel data, string title)
    {
        LoadTemplate("TraderPerformanceSoccer.xlsx", false);
        BuildTitle(title, "A1:DS2");
        var startIndexOfTotal = data.ReportRows.Count + StartRowIndex;
        BuildBodyRows(data.ReportRows, StartRowIndex);
        BuildTotalRow(data.GrandTotal, startIndexOfTotal);
        ClearTemplate(false);
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelForDetail(TraderPerformanceSoccerDetailModel data, string title)
    {
        LoadTemplate("TraderPerformanceSoccer_Detail.xlsx", true);
        BuildTitle(title, "A1:DS2");
        BuildDetailBodyRows(data.ReportRows, data.GrandTotal, StartRowIndex);
        ClearTemplate(true);
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel, bool isDetail)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TraderPerformanceSoccer/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        if (isDetail)
        {
            subTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
            sheet = ExcelPackage.Workbook.Worksheets["TraderPerformance_Soccer_Detail"];
        }
        else
        {
            sheet = ExcelPackage.Workbook.Worksheets["TraderPerformance_Soccer"];
        }
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IEnumerable<TraderPerformanceSoccerModel> list, int startRowIndex)
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

    private void BuildDetailBodyRows(IEnumerable<TraderPerformanceSoccerDetailModel> list, TraderPerformanceSoccerDetailModel grandTotal, int startRowIndex)
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

    private void BuildDetailTotalRow(TraderPerformanceSoccerDetailModel subTotal, int indexRow, string text, ExcelWorksheet template)
    {
        var excelModel = CreateDetailExcelModel(subTotal);
        sheet.ApplyModel(template, excelModel, indexRow);
        sheet.Cells[$"A{indexRow}:F{indexRow}"].Merge = true;
        sheet.Cells[$"A{indexRow}:F{indexRow}"].Value = text;
    }

    private static TraderPerformanceSoccerExcelModel CreateExcelModel(TraderPerformanceSoccerModel model)
    {
        var excelModel = new TraderPerformanceSoccerExcelModel
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

    private static TraderPerformanceSoccerDetailExcelModel CreateDetailExcelModel(TraderPerformanceSoccerDetailModel model)
    {
        var excelModel = new TraderPerformanceSoccerDetailExcelModel
        {
            EventDate = model.EventDate,
            League = model.LeagueName,
            Match = $"{model.HomeName}-{model.AwayName}",
            MaxBet = model.Maxbet == 0 ? null : model.Maxbet,
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
        type.GetProperty($"{category}FirstHalfOneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfOneXTwo")?.GetValue(data))));
        type.GetProperty($"{category}OneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("OneXTwo")?.GetValue(data))));
        type.GetProperty($"{category}FifteenMinOneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FifteenMinOneXTwo")?.GetValue(data))));
        type.GetProperty($"{category}TenMinOneXTwo")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TenMinOneXTwo")?.GetValue(data))));
        type.GetProperty($"{category}FirstHalfHdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfHdp")?.GetValue(data))));
        type.GetProperty($"{category}Hdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("Hdp")?.GetValue(data))));
        type.GetProperty($"{category}FifteenMinHdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FifteenMinHdp")?.GetValue(data))));
        type.GetProperty($"{category}TenMinHdp")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TenMinHdp")?.GetValue(data))));
        type.GetProperty($"{category}FirstHalfOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfOU")?.GetValue(data))));
        type.GetProperty($"{category}OU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("OU")?.GetValue(data))));
        type.GetProperty($"{category}FifteenMinOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FifteenMinOU")?.GetValue(data))));
        type.GetProperty($"{category}TenMinOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TenMinOU")?.GetValue(data))));
        type.GetProperty($"{category}SingleTeamOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("SingleTeamOU")?.GetValue(data))));
        type.GetProperty($"{category}FirstHalfNoOfCorners")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfNoOfCorners")?.GetValue(data))));
        type.GetProperty($"{category}NoOfCorners")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("NoOfCorners")?.GetValue(data))));
        type.GetProperty($"{category}FifteenMinNoOfCorners")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FifteenMinNoOfCorners")?.GetValue(data))));
        type.GetProperty($"{category}NextCorner")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("NextCorner")?.GetValue(data))));
        type.GetProperty($"{category}FirstHalfTotalBookings")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("FirstHalfTotalBookings")?.GetValue(data))));
        type.GetProperty($"{category}TotalBookings")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TotalBookings")?.GetValue(data))));
        type.GetProperty($"{category}TotalBookingsFifteenMinOU")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TotalBookingsFifteenMinOU")?.GetValue(data))));
        type.GetProperty($"{category}TotalBookingsFifteenMinHDP")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("TotalBookingsFifteenMinHDP")?.GetValue(data))));
        type.GetProperty($"{category}NextBookings")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("NextBookings")?.GetValue(data))));
        type.GetProperty($"{category}Overall")?.SetValue(model, FormatDecimalData(Convert.ToDecimal(dataType.GetProperty("Overall")?.GetValue(data))));
       
    }

    private static decimal? FormatDecimalData(decimal data)
    {
        return data == 0 ? null : data;
    }

    private void BuildTotalRow(TraderPerformanceSoccerModel grandTotal, int indexRow)
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