namespace HouseWinLoss.Common.MatchDetail;

using Criteria;

public interface WinLossService
{
    Task<WinLossModel> Query(WinLossCriteria criteria);
}

public class WinLossServiceImp(RawWinLossService rawWinLossService, CalculationService calculationService)
    : WinLossService
{
    public async Task<WinLossModel> Query(WinLossCriteria criteria)
    {
        var rawWinLoss = await rawWinLossService.Query(criteria);
        var leagues = calculationService.CalculateLeagues(rawWinLoss).ToList();
        var total = calculationService.CalculateSumWinLoss(rawWinLoss);
        var rm = calculationService.CalculateRM(rawWinLoss).ToList();
        var totalRows = rawWinLoss.Count(x => x.LeagueId > 0 && x.MatchId > 0);

        return new WinLossModel
        {
            Leagues = leagues,
            Total = total,
            RM = rm,
            TotalRows = totalRows
        };
    }
}

public class WinLossService3rdImp(RawWinLossService rawWinLossService, Calculation3rdServiceImp calculationService)
    : WinLossServiceImp(rawWinLossService, calculationService);