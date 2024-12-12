using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SuperBalance.Model;
using Utilities.Configurations;

namespace SuperBalance.Services;

public interface SuperBalanceExcelService
{
    Task<ExcelPackage> ExportExcelSuperBalance(SuperBalanceModel data, string title);
}

public class SuperBalanceExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : SuperBalanceExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 3;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelSuperBalance(SuperBalanceModel data, string title)
    {
        LoadTemplate("SuperBalance.xlsx");
        BuildTitle(title, "A1:E1");
        var startIndexOfTotal = data.SuperBalanceItems.Count + StartRowIndex;
        if (data.SuperBalanceItems.Count > 0)
        {
            BuildBodyRows(data.SuperBalanceItems, StartRowIndex);
        }
        else
        {
            startIndexOfTotal--;
        }
        BuildTotalRow(data.BaseBalanceTotal, startIndexOfTotal);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SuperBalance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["SuperBalance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IEnumerable<BalanceItemModel> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<BalanceItemExcelModel>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(BalanceItemModel baseBalanceTotal, int startIndex)
    {
        var item = new BalanceTotalItemExcelModel
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