using CompanyBalanceDifference.Criteria;
using CompanyBalanceDifference.Entities;
using CompanyBalanceDifference.Model;
using Fanex.Data.Repository;

namespace CompanyBalanceDifference.Services
{
    public interface CompanyBalanceDifferenceService
    {
        Task<List<CompanyBalanceDifferenceModel>> GetCompanyBalanceDifference();
    }
    public class CompanyBalanceDifferenceServiceImp(IDynamicRepository repository) : CompanyBalanceDifferenceService
    {
        public async Task<List<CompanyBalanceDifferenceModel>> GetCompanyBalanceDifference()
        {
            var criteria = new CompanyBalanceDifferenceCriteria();
            var rawData = await repository.FetchAsync<CompanyBalanceDifferenceItem>(criteria);

            var result = new List<CompanyBalanceDifferenceModel>();

            if (!rawData.Any())
            {
                return result;
            }

            result.AddRange(rawData.Select(row => new CompanyBalanceDifferenceModel { BalanceUpDate = $"{row.BalanceUpDay:MMM dd, yyyy}", BalanceBeforeBalanceUpDate = row.CompanyBalanceBefore, BalanceAfterBalanceUpDate = row.CompanyBalanceAfter, Difference = row.Difference }));

            return result;
        }
    }
}
