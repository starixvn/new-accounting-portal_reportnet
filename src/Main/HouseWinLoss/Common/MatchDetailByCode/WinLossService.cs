using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Models;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Common.MatchDetailByCode
{
    public interface WinLossService
    {
        Task<WinLossModel> Query(WinLossCriteria criteria);
    }

    public class WinLossServiceImp(
        RawWinLossService rawWinLossService,
        CalculationService calculationService)
        : WinLossService
    {
        public async Task<WinLossModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var leagues = rawWinLosses.Select(l => l.GroupName).Distinct();
            var leagueWinLosses = leagues.Select(l => CalculateLeagueWinLoss(l, rawWinLosses))
                                         .OrderBy(l => l.Name);
            var grandTotal = calculationService.CalculateSumWinLoss("Grand Total", rawWinLosses);
            var totalRows = rawWinLosses.Count();

            return new WinLossModel
            {
                Leagues = leagueWinLosses.ToList(),
                Total = grandTotal,
                TotalRows = totalRows
            };
        }

        private LeagueWinLoss CalculateLeagueWinLoss(string league, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var rawLeagueWinLosses = rawWinLosses.Where(r => r.GroupName == league);
            var matchWinLosses = rawLeagueWinLosses
                .Select(m => calculationService.FromRawWinLossByRowName(m))
                .OrderBy(m => m.Name);
            var total = calculationService.CalculateSumWinLoss(string.Empty, rawLeagueWinLosses);
            var totalRows = rawWinLosses.Count();

            return new LeagueWinLoss
            {
                Name = league,
                Matches = matchWinLosses.ToList(),
                Total = total,
                TotalRows = totalRows
            };
        }
    }
}