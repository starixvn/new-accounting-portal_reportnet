using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface SoccerPerformanceDetailExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data, GetBetTypePerformanceCriteria criteria);
}

public class SoccerPerformanceDetailExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : SoccerPerformanceDetailExcelService
{
    private ExcelWorksheet? sheet;

    private ExcelWorksheet? oddSheet;

    private ExcelWorksheet? evenSheet;

    private ExcelWorksheet? totalSheet;

    private const int StartRowIndex = 5;

    private ExcelPackage? ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data, GetBetTypePerformanceCriteria criteria)
    {
        LoadTemplate("BetType.xlsx");
        BuildFilterBlock(criteria);
        var bodyData = criteria.MarketFilter switch
        {
            Market.NonLive => data.ItemNonLiveList,
            Market.Live => data.ItemLiveList,
            _ => data.ItemByOrderList
        };
        BuildBodyRows(bodyData, StartRowIndex);
        var startTotalRowIndex = data.ItemByOrderList.Count + StartRowIndex;
        BuildTotalRow(data.TotalItemLive, startTotalRowIndex, ExcelConstants.GrandTotalText, ExcelConstants.LiveText);
        BuildTotalRow(data.TotalItemNonLive, startTotalRowIndex, string.Empty, ExcelConstants.NonLiveText);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildFilterBlock(GetBetTypePerformanceCriteria criteria)
    {
        var richText = sheet!.Cells["A2:O2"].RichText;
        richText.Add("Market: ").Bold = true;
        richText.Add(criteria.MarketFilter.ToString());
        richText.Add(Environment.NewLine);
        richText.Add("From Date: ").Bold = true;
        richText.Add($"{criteria.FromDate:MM/dd/yyyy}");
    }

    private void BuildBodyRows(IEnumerable<BetTypePerformanceItem> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<BetTypePerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            item.Market = item.IsLive ? ExcelConstants.LiveText : ExcelConstants.NonLiveText;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(BetTypePerformanceItem data, int startIndex, string totalText, string liveText)
    {
        var excelModel = mapper.Map<BetTypePerformanceExcelItem>(data);
        excelModel.Market = liveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        sheet!.Cells[$"A{startIndex}:B{startIndex}"].Value = totalText;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/BetType/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Bet Type Performance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage!.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("TotalTemplate");
    }
}