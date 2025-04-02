using AutoMapper;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface LeagueExcelService
{
    Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data,
        GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class LeagueExcelServiceImp(ConfigurationService configurationService, IMapper mapper)
    : BaseExcelService(configurationService), LeagueExcelService
{

    public Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data,
        GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("League", "League.xlsx", ExcelConstants.LeaguePerformance);
        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var startBodyRowIndex = rowIndex + 4;
        var bodyData = criteria.MarketFilter switch
        {
            Market.NonLive => data.ItemNonLiveList,
            Market.Live => data.ItemLiveList,
            _ => data.ItemByOrderList
        };
        BuildBodyRows<LeaguePerformanceItem, LeaguePerformanceExcelItem>(bodyData, startBodyRowIndex, mapper);
        var startTotalRowIndex = data.ItemByOrderList.Count + startBodyRowIndex;
        BuildTotalRow<LeaguePerformanceItem, LeaguePerformanceExcelItem>(data.TotalItemLive, data.TotalItemNonLive, startTotalRowIndex, mapper);
        ClearTemplate(ExcelConstants.LeaguePerformance);
        return Task.FromResult(ExcelPackage!);
    }
}