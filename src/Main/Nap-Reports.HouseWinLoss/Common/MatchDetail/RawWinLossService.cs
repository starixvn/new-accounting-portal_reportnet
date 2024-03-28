using Fanex.Data.Repository;
using Nap_Reports.HouseWinLoss.Common.Criteria;

namespace Nap_Reports.HouseWinLoss.Common.MatchDetail
{
    public interface RawWinLossService
    {
       Task<List<RawWinLoss>> Query(WinLossCriteria criteria);
    }

    public class RawWinLossServiceImp : RawWinLossService
    {
        private readonly IDynamicRepository repository;

        public RawWinLossServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public virtual async Task<List<RawWinLoss>> Query(WinLossCriteria criteria)
        {
            return (await repository.FetchAsync<RawWinLoss>(criteria)).ToList();
        }
    }
}