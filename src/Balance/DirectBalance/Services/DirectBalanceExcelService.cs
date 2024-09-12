using AutoMapper;
using DirectBalance.Model;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;

namespace DirectBalance.Services;

public interface DirectBalanceExcelService
{
    Task<ExcelPackage> ExportExcelDirectBalance(DirectBalanceModel data, string level);
}

public class DirectBalanceExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : DirectBalanceExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 3;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelDirectBalance(DirectBalanceModel data, string level)
    {
        LoadTemplate("DirectBalance.xlsx");
        BuildTitle($"{level} Balance", "A1:E1");
        var startIndexOfTotal = data.DirectBalanceItems.Count() + StartRowIndex;
        BuildBodyRows(data.DirectBalanceItems, StartRowIndex);
        BuildTotalRow(data.Total, startIndexOfTotal);
        sheet.Cells["B2"].Value = $"{level} Balance";
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/DirectBalance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["DirectBalance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IEnumerable<DirectBalanceItemModel> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<DirectBalanceItemExcelModel>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(DirectBalanceItemModel baseBalanceTotal, int startIndex)
    {
        var item = new DirectBalanceTotalItemExcelModel()
        {
            Text = "Total",
            Total = baseBalanceTotal.BaseBalance
        };
        sheet.ApplyModel(totalSheet, item, startIndex);
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}