using Fanex.Data.Repository;
using NegativeBalance.Criteria;
using NegativeBalance.Model;

namespace NegativeBalance.Services
{
    public interface NegativeBalanceService
    {
        Task<NegativeBalanceModel> GetNegativeBalance(NegativeBalanceCriteria negativeBalanceCriteria);

        Task<NegativeBalanceModel> GetAllNegativeBalance(NegativeBalanceCriteria negativeBalanceCriteria);

        Task<List<LicenseeModel>> GetLicensee();
    }

    public class NegativeBalanceServiceImp(IDynamicRepository repository) : NegativeBalanceService
    {
        public async Task<List<LicenseeModel>> GetLicensee()
        {
            var criteria = new LicenseeCriteria();
           
            var data = (await repository.FetchAsync<LicenseeModel>(criteria)).ToList();

            return data;
        }

        public async Task<NegativeBalanceModel> GetNegativeBalance(NegativeBalanceCriteria criteria)
        {
            var model = new NegativeBalanceModel();
            var rawData =  (await repository.FetchAsync<NegativeBalanceUser>(criteria)).ToList();
            
            List<NegativeBalanceUser> tempUsers = [];
            
            if (rawData != null && rawData.Any())
            {
                var from = criteria.PageSize * (criteria.PageIndex - 1);
                var to = criteria.PageSize * criteria.PageIndex;
                
                for (var i = from; i < rawData.Count && i < to; i++)
                {
                    tempUsers.Add(rawData[i]);
                }

                SiteNegativeBalanceModel currentNegative = null;

                var tempUsersOderBySite = tempUsers.OrderBy(negativeBalanceUser => negativeBalanceUser.Site).ToList();


                tempUsersOderBySite.ForEach(data =>
                    {
                        if (currentNegative == null || currentNegative.LicenseeSite != data.Site)
                        {
                            currentNegative = new SiteNegativeBalanceModel
                            {
                                LicenseeSite = data.Site,
                                NegativeBalanceUsers = []
                            };

                            model.NegativeBalanceUsers.Add(currentNegative);
                        }

                        currentNegative.NegativeBalanceUsers.Add(data);
                    });

                model.TotalNegativeBalanceUser = rawData.Count;
            }

            return model;
        }

        public async Task<NegativeBalanceModel> GetAllNegativeBalance(NegativeBalanceCriteria criteria)
        {
            var model = new NegativeBalanceModel();
            var rawData = (await repository.FetchAsync<NegativeBalanceUser>(criteria)).ToList();
            SiteNegativeBalanceModel currentNegative = null;
            var tempUsersOderBySite = rawData.OrderBy(negativeBalanceUser => negativeBalanceUser.Site).ToList();
            tempUsersOderBySite.ForEach(data =>
            {
                if (currentNegative == null || currentNegative.LicenseeSite != data.Site)
                {
                    currentNegative = new SiteNegativeBalanceModel
                    {
                        LicenseeSite = data.Site,
                        NegativeBalanceUsers = []
                    };

                    model.NegativeBalanceUsers.Add(currentNegative);
                }

                currentNegative.NegativeBalanceUsers.Add(data);
            });

            model.TotalNegativeBalanceUser = rawData.Count;
            return model;
        }
    }
}
