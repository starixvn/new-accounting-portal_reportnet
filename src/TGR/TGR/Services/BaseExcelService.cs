using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria.SoccerPerformanceDetail;
using TGR.Utilities;
using TGR.ViewModel.SoccerPerformanceDetail;
using Utilities.Configurations;

namespace TGR.Services;

public abstract class BaseExcelService(ConfigurationService configurationService)
{
    protected ExcelWorksheet sheet;
    protected ExcelWorksheet oddSheet;
    protected ExcelWorksheet evenSheet;
    protected ExcelWorksheet totalSheet;
    protected ExcelPackage ExcelPackage;
    protected ExcelWorksheet titleSheet;
    protected ExcelWorksheet bodySheet;
    protected const int StartFilterBlockRowIndex = 3;

    protected int BuildFilterBlock<T>(T criteria, IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
        where T : IBasePerformanceCriteria
    {
        var rowIndex = StartFilterBlockRowIndex;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.MarketFilter;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.FromDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.ToDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value =
            ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetTypes);
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = FilterBeautyMapper.BeautyMatchGroup(criteria.MatchGroup);
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = FilterBeautyMapper.BeautyVrSetting(criteria.VrSettings);
        var lineCount = (int)Math.Ceiling((double)sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Text.Length / ExcelConstants.Width);
        if (lineCount > 1)
        {
            sheet.Row(rowIndex).Height = 8 * lineCount;
        }

        sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Value =
            ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);

        return rowIndex;
    }

    protected void LoadTemplate(string category, string fileExcel, string mainSheetName)
    {
        var templatePath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/{category}/{fileExcel}";
        using var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets[mainSheetName];
        if (mainSheetName is ExcelConstants.TraderPerformance or ExcelConstants.TeamPerformance or ExcelConstants.GradePerformance)
        {
            titleSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.TitleTemplate];
            bodySheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.BodyTemplate];
        }
        else
        {
            evenSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.EvenTemplate];
            oddSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.OddTemplate];
        }
        totalSheet = ExcelPackage.Workbook.Worksheets[ExcelConstants.TotalTemplate];
    }

    protected void ClearTemplate(string mainSheetName)
    {
        if (mainSheetName is ExcelConstants.TraderPerformance or ExcelConstants.TeamPerformance or ExcelConstants.GradePerformance)
        {
            ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.TitleTemplate);
            ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.BodyTemplate);
        }
        else
        {
            ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.EvenTemplate);
            ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.OddTemplate);
        }
        ExcelPackage!.Workbook.Worksheets.Delete(ExcelConstants.TotalTemplate);
    }


    protected void BuildBodyRows<TData, TExcelModel>(IEnumerable<TData> list, int startRowIndex, IMapper mapper)
        where TExcelModel : BasePerformanceExcelItem
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<TExcelModel>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            item.Market = item.IsLive ? ExcelConstants.LiveText : ExcelConstants.NonLiveText;
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    protected void BuildTotalRow<TData, TExcelModel>(TData liveData, TData nonLivedata, int startIndex, IMapper mapper, Market market)
        where TExcelModel : BasePerformanceExcelItem
    {
        switch (market)
        {
            case Market.Live:
                AddMarketTotalRow<TData, TExcelModel>(liveData, startIndex, mapper, ExcelConstants.LiveText);
                break;
            case Market.NonLive:
                AddMarketTotalRow<TData, TExcelModel>(nonLivedata, startIndex, mapper, ExcelConstants.NonLiveText);
                break;
            case Market.All:
                AddMarketTotalRow<TData, TExcelModel>(liveData, startIndex, mapper, ExcelConstants.LiveText);
                AddMarketTotalRow<TData, TExcelModel>(nonLivedata, startIndex + 1, mapper, ExcelConstants.NonLiveText);

                var cellTotalAddress = $"A{startIndex}:B{startIndex + 1}";
                sheet!.Cells[cellTotalAddress].Merge = true;
                sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
                return;
        }

        var singleRowTotalAddress = $"A{startIndex}:B{startIndex}";
        sheet!.Cells[singleRowTotalAddress].Merge = true;
        sheet!.Cells[singleRowTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void AddMarketTotalRow<TData, TExcelModel>(TData data, int rowIndex, IMapper mapper, string marketText)
        where TExcelModel : BasePerformanceExcelItem
    {
        var excelModel = mapper.Map<TExcelModel>(data);
        excelModel.Market = marketText;
        sheet.ApplyModel(totalSheet, excelModel, rowIndex);
        UpdateValueSpecificColumns(rowIndex, excelModel);
    }

    protected void UpdateValueSpecificColumns<TExcelModel>(int startIndex, TExcelModel model)
        where TExcelModel : BasePerformanceExcelItem
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{startIndex}"], model.MarginReal);
        sheet!.Cells[$"I{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"M{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }

    protected delegate void BuildBodyRowsDelegate<T>(IEnumerable<T> items, int startRowIndex, string title);

    protected int ProcessDataList<T>(IEnumerable<T> items, int startRowIndex, string title, BuildBodyRowsDelegate<T> buildBodyRowsMethod)
    {
        if (!items.Any())
        {
            return startRowIndex;
        }

        buildBodyRowsMethod(items, startRowIndex, title);
        return startRowIndex + items.Count() + 1;
    }

    protected void HideNonPermissionColumn(bool allowVirtual, bool allowReal)
    {
        var rowCount = sheet!.Dimension.End.Row;
        if (!allowVirtual)
        {
            sheet!.Cells[$"H1:K{rowCount}"].Value = null;
            sheet!.Cells[$"H1:K{rowCount}"].Value = null;
            sheet.Columns[8, 11].Width = 0;
            sheet.Columns[8, 11].Hidden = true;
        }
        if (!allowReal)
        {
            sheet!.Cells[$"L1:O{rowCount}"].Value = null;
            sheet.Columns[12, 15].Width = 0;
            sheet.Columns[12, 15].Hidden = true;
        }
    }
}