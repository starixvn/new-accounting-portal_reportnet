using AutoMapper;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria;
using TGR.ViewModel;
using Utilities.Configurations;

namespace TGR.Services;

public interface LeagueExcelService
{
    Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data,
        GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance, 
        bool allowRealPerformance);
}

public class LeagueExcelServiceImp(ConfigurationService configurationService, IMapper mapper)
    : BaseExcelService(configurationService), LeagueExcelService
{

    public Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data,
        GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance,
        bool allowRealPerformance)
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
        var startTotalRowIndex = bodyData.Count + startBodyRowIndex;
        BuildTotalRow<LeaguePerformanceItem, LeaguePerformanceExcelItem>(data.TotalItemLive, data.TotalItemNonLive, startTotalRowIndex, mapper, criteria.MarketFilter);

        HideNonPermissionColumn(allowVirtualPerformance, allowRealPerformance);

        ClearTemplate(ExcelConstants.LeaguePerformance);
        return Task.FromResult(ExcelPackage!);
    }
}