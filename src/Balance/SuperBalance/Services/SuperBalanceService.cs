using Fanex.Data.Repository;
using Utilities.Currencies;
using SuperBalance.Criteria;
using SuperBalance.Model;
using Utilities.LinqDynamicSum;

namespace SuperBalance.Service
{
    public interface SuperBalanceService
    {
        Task<SuperBalanceModel> GetSuperBalance(SuperBalanceCriteria criteria);

        Task<SuperBalanceModel> GetSuperBalanceDownLine(SuperDownlineCriteria criteria);

        Task<SuperBalanceModel> GetMasterDownLine(MasterDownlineCriteria criteria);

        Task<SuperBalanceModel> GetAgentDownLine(AgentDownlineCriteria criteria);

        Task<List<CurrenciesModel>> GetCurrencies();

        List<string> GetListAlphabet();
    }

    public class SuperBalanceServiceImp(IDynamicRepository repository, CurrenciesService currenciesService) : SuperBalanceService
    {
        private enum ReportRowType
        {
            Total = 1,

            GrandTotal = 2,

            NormalEvenRow = 3,

            NormalOddRow = 4
        }

        private enum BalanceType
        {
            Today = 1,

            Yesterday = 2
        }

        
        public async Task<SuperBalanceModel> GetSuperBalance(SuperBalanceCriteria criteria)
        {
            var result = new SuperBalanceModel
            {
                SuperBalanceItems = new List<BalanceItemModel>(),
                BaseBalanceTotal = new BalanceItemModel(),
            };

            if (criteria.Currency == 0)
            {
                criteria.Currency = null;
            }

            if (criteria.Username == null)
            {
                criteria.Username = string.Empty;
            }

            if (criteria.FirstLetter == null)
            {
                criteria.FirstLetter = string.Empty;
            }

            var rawData = await repository.FetchAsync<BalanceItemModel>(criteria);

            if (rawData == null)
            {
                return result;
            }

            if (criteria.IsViewTotalOnly)
            {
                var total = criteria.Total;
               
                var balanceTotal = new BalanceItemModel()
                {
                    CustId = null,
                    Username = null,
                    Currency = null,
                    CurrencyName = null,
                    Balance = 0,
                    BaseBalance = total
                };

                result.BaseBalanceTotal = balanceTotal;
                result.HasData = !Convert.IsDBNull(total);
            }
            else
            {
                result.SuperBalanceItems = rawData.Where(item => Math.Round(item.Balance, 2, MidpointRounding.AwayFromZero) != 0
                                                                 && Math.Round(item.BaseBalance, 2, MidpointRounding.AwayFromZero) != 0).ToList();
                result.BaseBalanceTotal = result.SuperBalanceItems.DynamicSum();

            }

            return result;
        }


        public async Task<SuperBalanceModel> GetSuperBalanceDownLine(SuperDownlineCriteria criteria)
        {
            var result = await GetBalanceDownline(criteria);

            return result;
        }


        public async Task<SuperBalanceModel>GetMasterDownLine(MasterDownlineCriteria criteria)
        {
            var result = await GetBalanceDownline(criteria);

            return result;
        }


        public async Task<SuperBalanceModel> GetAgentDownLine(AgentDownlineCriteria criteria)
        {
            var result = await GetBalanceDownline(criteria);

            return result;
        }

        public async Task<List<CurrenciesModel>> GetCurrencies()
        {
            var result = await currenciesService.GetCurrencies();

            return result.OrderBy(x => x.CurrencyName).ToList();
        }

        public List<string> GetListAlphabet()
        {
            var arrLetter = new List<string>();
            arrLetter.Add("All");
            arrLetter.Add("0-9");
            for (var i = 'A'; i <= 'Z'; i++)
            {
                arrLetter.Add(((char)i).ToString());
            }

            return arrLetter;
        }

        private string GetBalanceType(int balanceTypeVal)
        {
            var balanceType = string.Empty;

            if (balanceTypeVal == (int)BalanceType.Today)
            {
                balanceType = "Today";
            }
            else
            {
                balanceType = "Yesterday";
            }

            return balanceType;
        }

        private async Task<SuperBalanceModel> GetBalanceDownline(CriteriaBase criteria)
        {
            var result = new SuperBalanceModel
            {
                SuperBalanceItems = new List<BalanceItemModel>(),
                BaseBalanceTotal = new BalanceItemModel(),
            };


            var rawData = await repository.FetchAsync<BalanceItemModel>(criteria);

            if (rawData == null)
            {
                return result;
            }

            result.SuperBalanceItems = rawData.Where(item => Math.Round(item.Balance, 2, MidpointRounding.AwayFromZero) != 0
                                                             && Math.Round(item.BaseBalance, 2, MidpointRounding.AwayFromZero) != 0).ToList();
            result.BaseBalanceTotal = result.SuperBalanceItems.DynamicSum();

            return result;
        }
    }
}
