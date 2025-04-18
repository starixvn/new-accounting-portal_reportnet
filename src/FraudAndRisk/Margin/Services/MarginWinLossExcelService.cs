namespace Margin.Services;

using AutoMapper;
using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface MarginWinLossExcelService
{
    Task<ExcelPackage> ExportExcel(List<MarginWinLossModel> data, string title);
}

public class MarginWinLossExcelServiceImp(IMapper mapper, ConfigurationService configurationService) : MarginWinLossExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(List<MarginWinLossModel> data, string title)
    {
        var excelData = mapper.Map<List<MarginWinLossExcelModel>>(data);
        LoadTemplate("MarginReport.xlsx");
        BuildTitle(title, "A1:G1");
        BuildBody(excelData, StartRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string templateName)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/Margin/{templateName}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["MarginReport"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBody(List<MarginWinLossExcelModel> data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        var rowDataNumber = 1;

        foreach (var item in data)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = rowDataNumber++;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }
       

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
    }
}