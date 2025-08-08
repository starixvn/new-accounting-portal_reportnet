using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria.SoccerPerformanceDetail;
using TGR.Utilities;
using TGR.ViewModel.JuiceLeftOver;
using TGR.ViewModel.SoccerPerformanceDetail;
using Utilities.Configurations;

namespace TGR.Services.JuiceLeftOver;

public abstract class BaseExcelService(ConfigurationService configurationService)
{
    protected ExcelWorksheet sheet;
    protected ExcelWorksheet oddSheet;
    protected ExcelWorksheet evenSheet;
    protected ExcelWorksheet totalSheet;
    protected ExcelPackage ExcelPackage;

    protected void LoadTemplate(string category, string fileExcel, string mainSheetName)
    {
        var templatePath = $"{configurationService.CurrentDirectory}/ExcelTemplate/PerformanceDetailJuiceLeftover/{category}/{fileExcel}";
        using var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets[mainSheetName];
        evenSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.EvenTemplate];
        oddSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.OddTemplate];
        totalSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.TotalTemplate];
    }

    protected void ClearTemplate(string mainSheetName)
    {
        ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.EvenTemplate);
        ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.OddTemplate);
        ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.TotalTemplate);
    }

    protected void BuildTitle(DateTime fromDate, DateTime toDate, ExcelRange excelRange)
    {
        excelRange.Value = $"From Date: {fromDate:MM/dd/yyyy} - To Date: {toDate:MM/dd/yyyy}";
    }

    protected void BuildBodyRows<TData, TExcelModel>(IEnumerable<TData> list, int startRowIndex, IMapper mapper)
        where TExcelModel : BaseExcelItem
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<TExcelModel>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    protected void UpdateValueSpecificColumns<TExcelModel>(int rowIndex, TExcelModel model)
        where TExcelModel : BaseExcelItem
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"P{rowIndex}"], model.Margin);
    }

    protected void BuildTotalRow<TData, TExcelModel>(TData data, int startIndex, IMapper mapper)
        where TExcelModel : BaseExcelItem
    {
        var excelModel = mapper.Map<TExcelModel>(data);
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        var singleRowTotalAddress = $"A{startIndex}:B{startIndex}";
        sheet!.Cells[singleRowTotalAddress].Merge = true;
        sheet!.Cells[singleRowTotalAddress].Value = ExcelConstants.GrandTotalText;
    }
}