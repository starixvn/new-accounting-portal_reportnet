namespace HouseWinLoss.MetricGaming.LiveNonLive;

using Common.BasicWinLoss;
using Common.Criteria;
using Common.Services;

public interface MetricGamingLiveNonLiveWinLossService
{
    Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria);
}

public class MetricGamingLiveNonLiveWinLossServiceImp(
    RawWinLossService rawWinLossService,
    CalculationService calculationService)
    : MetricGamingLiveNonLiveWinLossService
{
    public async Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria)
    {
        var rawWinLosses = await rawWinLossService.Query(criteria);

        var betTypeRawWinLosses = rawWinLosses.Where(r => !r.GroupName.Equals("Metric Gaming - Superlive Live", StringComparison.CurrentCultureIgnoreCase)
                                                          && !r.GroupName.Equals("Metric Gaming - Superlive Non-Live", StringComparison.CurrentCultureIgnoreCase));
        var betTypeSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(betTypeRawWinLosses);
        var betTypeGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", betTypeRawWinLosses);

        var betTypeSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = betTypeSiteWinLossSummaries.ToList(),
            GrandTotal = betTypeGrandTotal
        };

        var liveNonLiveRawWinLosses = rawWinLosses.Where(r => r.GroupName.Equals("Metric Gaming - Superlive Live", StringComparison.CurrentCultureIgnoreCase)
                                                              || r.GroupName.Equals("Metric Gaming - Superlive Non-Live", StringComparison.CurrentCultureIgnoreCase));
        var liveNonLiveSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(liveNonLiveRawWinLosses);
        var liveNonLiveGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", liveNonLiveRawWinLosses);

        var liveNonLiveSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = liveNonLiveSiteWinLossSummaries.ToList(),
            GrandTotal = liveNonLiveGrandTotal
        };

        return [betTypeSummaries, liveNonLiveSummaries];
    }
}