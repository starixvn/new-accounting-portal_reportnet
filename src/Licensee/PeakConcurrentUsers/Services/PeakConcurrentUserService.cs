using Fanex.Data.Repository;
using PeakConcurrentUsers.Criteria;
using PeakConcurrentUsers.Entities;
using PeakConcurrentUsers.Model;

namespace PeakConcurrentUsers.Services
{
    public interface PeakConcurrentUserService
    {
        Task<List<LicenseeModel>> GetLicensee();

        Task<PeakConcurrentUser> GetConcurrentUserList(PeakConcurrentUserCriteria criteria);
    }

    public class PeakConcurrentUserServiceImp(IDynamicRepository repository) : PeakConcurrentUserService
    { 
        public async Task<PeakConcurrentUser> GetConcurrentUserList(PeakConcurrentUserCriteria criteria)
        {
            var peakConcurrentUser = new PeakConcurrentUser();

            criteria.ToDate = criteria.ToDate.AddDays(1).AddSeconds(-1);
            
            var rawData = await repository.FetchAsync<PeakConcurentUserItem>(criteria);
            
            if (rawData != null && rawData.Any())
            {
                var concurrentUsers = new List<ConcurrentUser>();
                double totalUser = 0;

                foreach (var row in rawData)
                {
                    var concurrentUser = new ConcurrentUser()
                    {
                        Date = $"{row.LogDate:MM/dd/yyyy}",
                        TotalUser = $"{row.TotalUser:N0}"
                    };

                    concurrentUsers.Add(concurrentUser);
                    totalUser += row.TotalUser;
                }

                peakConcurrentUser.ListConcurrentUser = concurrentUsers;
                peakConcurrentUser.AverageUsers = $"{totalUser / concurrentUsers.Count:N2}";
                peakConcurrentUser.Total = $"{totalUser:N0}";
                peakConcurrentUser.MinUsers = $"{rawData.Select(x => x.TotalUser).Min():N0}";
                peakConcurrentUser.MaxUsers = $"{rawData.Select(x => x.TotalUser).Max():N0}";
            }



            return peakConcurrentUser;
        }

        public async Task<List<LicenseeModel>> GetLicensee()
        {
            var criteria = new LicenseeCriteria();
           
            var result = (await repository.FetchAsync<LicenseeModel>(criteria)).ToList();

            return result;
        }
    }
}
