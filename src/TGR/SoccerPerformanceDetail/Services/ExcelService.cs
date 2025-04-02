namespace SoccerPerformanceDetail.Services;

using AutoMapper;
using Constants;
using Criteria;
using global::Utilities.Configurations;
using OfficeOpenXml;
using ViewModel;

public interface ExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class ExcelServiceImp(ConfigurationService configurationService, IMapper mapper)
    : BaseExcelService(configurationService), ExcelService
{

    public Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("BetType", "BetType.xlsx", ExcelConstants.BetTypePerformance);
        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var startBodyRowIndex = rowIndex + 4;
        var bodyData = criteria.MarketFilter switch
        {
            Market.NonLive => data.ItemNonLiveList,
            Market.Live => data.ItemLiveList,
            _ => data.ItemByOrderList
        };
        BuildBodyRows<BetTypePerformanceItem, BetTypePerformanceExcelItem>(bodyData, startBodyRowIndex, mapper);
        var startTotalRowIndex = data.ItemByOrderList.Count + startBodyRowIndex;
        BuildTotalRow<BetTypePerformanceItem, BetTypePerformanceExcelItem>(data.TotalItemLive, data.TotalItemNonLive, startTotalRowIndex, mapper);
        ClearTemplate(ExcelConstants.BetTypePerformance);
        return Task.FromResult(ExcelPackage!);
    }
}