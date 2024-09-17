using AutoMapper;
using DailyCreatedCustomers.Model;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;

namespace DailyCreatedCustomers.Services;

public interface DailyCreatedCustomersExcelService
{
    Task<ExcelPackage> ExportExcel(SummaryModel data, string title);

    Task<ExcelPackage> ExportExcelDetail(IEnumerable<DetailModel> data, string title);
}

public class DailyCreatedCustomersExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : DailyCreatedCustomersExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet? totalSheet;

    private const int StartRowIndex = 5;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(SummaryModel data, string title)
    {
        LoadTemplate("DailyCreatedCustomer.xlsx", false);
        BuildTitle(title, "A1:G1");
        if (data.Summaries.Count != 0)
        {
            BuildBodyRows(data.Summaries, StartRowIndex);
            BuildTotalRow(data.Total, data.Summaries.Count + StartRowIndex);
        }
        ClearTemplate(false);
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelDetail(IEnumerable<DetailModel> data, string title)
    {
        LoadTemplate("DetailDailyCreatedCustomer.xlsx", true);
        BuildTitle(title, "A1:D1");
        if (data.Any())
        {
            BuildDetailBodyRows(data, StartRowIndex);
        }
        ClearTemplate(true);
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel, bool isDetail)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/DailyCreatedCustomer/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Data"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = isDetail ? null : ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IEnumerable<SummaryItemModel> list, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<SummaryItemExcelModel>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildDetailBodyRows(IEnumerable<DetailModel> data, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<DetailExcelModel>>(data);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(SummaryItemModel data, int startIndex)
    {
        var excelModel = mapper.Map<SummaryItemExcelModel>(data);
        excelModel.CreatedDate = "Total";
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
    }

    private void ClearTemplate(bool isDetail)
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        if (!isDetail)
        {
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}