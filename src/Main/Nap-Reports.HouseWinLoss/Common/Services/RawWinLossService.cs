using Fanex.Data.Repository;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Common.Services
{
    public interface RawWinLossService
    {
        Task<List<WinLossRaw>> Query(WinLossCriteria criteria);
    }

    public class RawWinLossServiceImp : RawWinLossService
    {
        private readonly IDynamicRepository repository;


        public RawWinLossServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public virtual async Task<List<WinLossRaw>> Query(WinLossCriteria criteria)
        {
            return (await repository.FetchAsync<WinLossRaw>(criteria)).ToList();
        }
    }
}