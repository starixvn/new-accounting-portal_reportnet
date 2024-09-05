using DirectBalance.Criteria;
using DirectBalance.Model;
using Fanex.Data.Repository;
using Nap_Reports.Utilities.Currencies;
using Utilities.LinqDynamicSum;

namespace DirectBalance.Service
{
    public interface DirectBalanceService
    {
        Task<List<CurrenciesModel>> GetCurrencies();

        Task<DirectBalanceModel> GetDirectBalance(DirectBalanceCriteria criteria);

        Task<DirectBalanceModel> GetDirectBalanceMasterDownLine(MasterDownlineCriteria criteria);

        Task<DirectBalanceModel> GetDirectBalanceAgentDownLine(AgentDownlineCriteria criteria);
    }
    public class DirectBalanceServiceImp(IDynamicRepository repository, CurrenciesService currenciesService) : DirectBalanceService
    {
        public async Task<DirectBalanceModel> GetDirectBalance(DirectBalanceCriteria criteria)
        {
            var result = new DirectBalanceModel();
           
            if (criteria.Currency == 0)
            {
                criteria.Currency = null;
            }

            if (string.IsNullOrEmpty(criteria.Username))
            {
                criteria.Username = string.Empty;
            }

            var rawData = await repository.FetchAsync<DirectBalanceItemModel>(criteria);

            if (rawData != null)
            {
                result.DirectBalanceItems = rawData;
                
                var total = rawData.DynamicSum();
                result.Total = total;
            }

            return result;
        }

        public async Task<DirectBalanceModel> GetDirectBalanceMasterDownLine(MasterDownlineCriteria criteria)
        {
            var result = await GetBalanceDownline(criteria);

            return result;
        }

        public async Task<DirectBalanceModel> GetDirectBalanceAgentDownLine(AgentDownlineCriteria criteria)
        {
            var result = await GetBalanceDownline(criteria);

            return result;
        }

        private async Task<DirectBalanceModel> GetBalanceDownline(CriteriaBase criteria)
        {
            var result = new DirectBalanceModel();

            var rawData = await repository.FetchAsync<DirectBalanceItemModel>(criteria);

            if (rawData != null)
            {
                result.DirectBalanceItems = rawData.Where(item => item.Balance != 0);

                var total = rawData.DynamicSum();
                result.Total = total;
            }

            return result;
        }

        public async Task<List<CurrenciesModel>> GetCurrencies()
        {
            var result = await currenciesService.GetCurrencies();

            return result.OrderBy(x => x.CurrencyName).ToList();
        }
    }
}
