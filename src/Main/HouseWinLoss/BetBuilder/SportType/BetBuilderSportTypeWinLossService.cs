using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.BetBuilder.SportType;

public interface BetBuilderSportTypeWinLossService
{
    Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria);
}

public class BetBuilderSportTypeWinLossServiceImp : BetBuilderSportTypeWinLossService
{
    private readonly RawWinLossService rawWinLossService;

    private readonly CalculationService calculationService;

    public BetBuilderSportTypeWinLossServiceImp(
        RawWinLossService rawWinLossService,
        CalculationService calculationService)
    {
        this.rawWinLossService = rawWinLossService;
        this.calculationService = calculationService;
    }

    public async Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria)
    {
        var rawWinLosses = await rawWinLossService.Query(criteria);

        var sportTypeRawWinLosses = rawWinLosses.Where(r => !r.GroupName.Contains("Live")
                                                            && !r.GroupName.Contains("Non-Live"));
        var sportTypeSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(sportTypeRawWinLosses);
        var sportTypeGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", sportTypeRawWinLosses);

        var sportTypeSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = sportTypeSiteWinLossSummaries.ToList(),
            GrandTotal = sportTypeGrandTotal
        };

        var liveNonLiveRawWinLosses = rawWinLosses.Where(r => r.GroupName.Contains("Live")
                                                              || r.GroupName.Contains("Non-Live"));
        var liveNonLiveSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(liveNonLiveRawWinLosses);
        var liveNonLiveGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", liveNonLiveRawWinLosses);

        var liveNonLiveSummaries = new BasicWinLossModel
        {
            SiteWinLossSummaries = liveNonLiveSiteWinLossSummaries.ToList(),
            GrandTotal = liveNonLiveGrandTotal
        };
        return new BasicWinLossModel[] { sportTypeSummaries, liveNonLiveSummaries };
    }
}