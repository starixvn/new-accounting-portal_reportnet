using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Soccer.LiveNonLive;

public interface SoccerLiveNonLiveWinLossService
{
    Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria);
}

public class SoccerLiveNonLiveWinLossServiceImp(
    RawWinLossService rawWinLossService,
    CalculationService calculationService)
    : SoccerLiveNonLiveWinLossService
{
    public async Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria)
    {
        var rawWinLosses = await rawWinLossService.Query(criteria);

        var majorMinorRawWinLosses = rawWinLosses.Where(r => r.GroupName.Contains("Soccer-Major Bet Types")
                                                             || r.GroupName.Contains("Soccer-Minor Bet Types")
                                                             || r.GroupName.Contains("Soccer PinGoal-Major Bet Types")
                                                             || r.GroupName.Contains("Soccer PinGoal-Minor Bet Types"));

        var majorMinorSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(majorMinorRawWinLosses);
        var majorMinorGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", majorMinorRawWinLosses);
        var majorMinorAllRMData = calculationService.CalculateRM(rawWinLosses);

        var majorMinorSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = majorMinorSiteWinLossSummaries.ToList(),
            RM = majorMinorAllRMData.ToList(),
            GrandTotal = majorMinorGrandTotal
        };

        var liveNonLiveRawWinLosses = rawWinLosses.Where(r => r.GroupName.Contains("Soccer Live")
                                                              || r.GroupName.Contains("Soccer Non-Live")
                                                              || r.GroupName.Contains("Soccer PinGoal Live")
                                                              || r.GroupName.Contains("Soccer PinGoal Non-Live"));

        var liveNonLiveSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(liveNonLiveRawWinLosses);
        var liveNonLiveGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", liveNonLiveRawWinLosses);
        var liveNonLiveAllRMData = calculationService.CalculateRM(rawWinLosses);

        var liveNonLiveSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = liveNonLiveSiteWinLossSummaries.ToList(),
            RM = liveNonLiveAllRMData.ToList(),
            GrandTotal = liveNonLiveGrandTotal
        };

        return [majorMinorSummaries,  liveNonLiveSummaries];
    }
}