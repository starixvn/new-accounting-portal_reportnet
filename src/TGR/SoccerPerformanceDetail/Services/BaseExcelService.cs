using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Utilities;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public abstract class BaseExcelService(ConfigurationService configurationService)
{
    protected ExcelWorksheet? sheet;
    protected ExcelWorksheet? oddSheet;
    protected ExcelWorksheet? evenSheet;
    protected ExcelWorksheet? totalSheet;
    protected ExcelPackage? ExcelPackage;
    protected ExcelWorksheet? titleSheet;
    protected ExcelWorksheet? bodySheet;
    protected const int StartFilterBlockRowIndex = 3;

    protected int BuildFilterBlock<T>(T criteria, IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes) 
        where T : IBasePerformanceCriteria
    {
        var rowIndex = StartFilterBlockRowIndex;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.MarketFilter;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.FromDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.ToDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Value = 
            ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetTypes);

        var lineCount = (int)Math.Ceiling((double)sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Text.Length / ExcelConstants.Width);
        if (lineCount > 1)
        {
            sheet.Row(rowIndex).Height = 8 * lineCount;
        }

        rowIndex = BuildPerformanceFilters(criteria, rowIndex);
        sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Value = 
            ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);

        return rowIndex;
    }

    protected string GenerateMarginString(decimal? marginFrom, decimal? marginTo)
    {
        var marginFromString = marginFrom != null ? marginFrom.ToString() : "min";
        var marginToString = marginTo != null ? marginTo.ToString() : "max";

        if (marginFrom == null)
            return $"≤ {marginTo}";
        return marginTo == null ? $"≥ {marginFrom}" : $"from {marginFromString} to {marginToString}";
    }

    protected void LoadTemplate(string category, string fileExcel, string mainSheetName)
    {
        var templatePath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/{category}/{fileExcel}";
        using var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets[mainSheetName];
        if (mainSheetName is ExcelConstants.TraderPerformance or ExcelConstants.TeamPerformance)
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
        if (mainSheetName is ExcelConstants.TraderPerformance or ExcelConstants.TeamPerformance)
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

    private int BuildPerformanceFilters<T>(T criteria, int rowIndex) where T : IBasePerformanceCriteria
    {
        rowIndex++;
        if (criteria.IsVirtual)
        {
            rowIndex = AddFilterRow(criteria.Turnover, rowIndex);
            rowIndex = AddFilterRow(criteria.WinLoss, rowIndex);
        }

        if (criteria.IsReal)
        {
            rowIndex = AddFilterRow(criteria.TurnoverReal, rowIndex);
            rowIndex = AddFilterRow(criteria.WinLossReal, rowIndex);
        }

        if (criteria.IsVirtual && (criteria.MarginFrom.HasValue || criteria.MarginTo.HasValue))
        {
            sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = 
                GenerateMarginString(criteria.MarginFrom, criteria.MarginTo);
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        if (criteria.IsReal && (criteria.MarginFromReal.HasValue || criteria.MarginToReal.HasValue))
        {
            sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = 
                GenerateMarginString(criteria.MarginFromReal, criteria.MarginToReal);
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        return rowIndex;
    }

    private int AddFilterRow(decimal? value, int rowIndex)
    {
        if (value.HasValue)
        {
            sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = value.Value.ToString("#,##0.00");
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }
        return rowIndex;
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

    protected void BuildTotalRow<TData, TExcelModel>(TData liveData, TData nonLivedata, int startIndex, IMapper mapper)
        where TExcelModel : BasePerformanceExcelItem
    {
        var excelModel = mapper.Map<TExcelModel>(liveData);
        excelModel.Market = ExcelConstants.LiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);

        excelModel = mapper.Map<TExcelModel>(nonLivedata);
        excelModel.Market = ExcelConstants.NonLiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex + 1);
        UpdateValueSpecificColumns(startIndex + 1, excelModel);

        var cellTotalAddress = $"A{startIndex}:B{startIndex + 1}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
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
} 