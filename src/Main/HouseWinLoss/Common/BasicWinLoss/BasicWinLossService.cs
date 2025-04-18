namespace HouseWinLoss.Common.BasicWinLoss;

using Criteria;
using Services;

public interface BasicWinLossService
{
    Task<BasicWinLossModel> Query(WinLossCriteria criteria);
}

public class BasicWinLossServiceImp(
    RawWinLossService rawWinLossService,
    CalculationService calculationService)
    : BasicWinLossService
{
    public async Task<BasicWinLossModel> Query(WinLossCriteria criteria)
    {
        var rawWinLosses = await rawWinLossService.Query(criteria);

        var siteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(rawWinLosses);
        var grandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", rawWinLosses);
        var allRMData = calculationService.CalculateRM(rawWinLosses);

        return new BasicWinLossModel
        {
            SiteWinLossSummaries = siteWinLossSummaries.ToList(),
            RM = allRMData.ToList(),
            GrandTotal = grandTotal
        };
    }
}