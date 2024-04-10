using HouseWinLoss.Common.Criteria;

namespace HouseWinLoss.Common.MatchDetail
{
    public interface WinLossService
    {
        Task<WinLossModel> Query(WinLossCriteria criteria);
    }

    public class WinLossServiceImp : WinLossService
    {
        private readonly RawWinLossService rawWinLossService;

        private readonly CalculationService calculationService;

        public WinLossServiceImp(RawWinLossService rawWinLossService, CalculationService calculationService)
        {
            this.rawWinLossService = rawWinLossService;
            this.calculationService = calculationService;
        }

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

    public class WinLossService3rdImp : WinLossServiceImp
    {
        public WinLossService3rdImp(RawWinLossService rawWinLossService, Calculation3rdServiceImp calculationService) : base(rawWinLossService, calculationService)
        {
        }
    }
}