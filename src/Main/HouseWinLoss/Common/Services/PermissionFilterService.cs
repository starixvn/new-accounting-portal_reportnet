using Fanex.Data.Repository;
using HouseWinLoss.Common.Criteria;

namespace HouseWinLoss.Common.Services
{
    public interface PermissionFilterService1
    {
        Task<int> GetPermissionProfileId(string functionKeyValue);
    }

    public class PermissionFilterServiceImp1 : PermissionFilterService1
    {
        private readonly IDynamicRepository repository;

        public PermissionFilterServiceImp1(IDynamicRepository repository)
        {
            this.repository = repository;
        }
     
        public async Task<int> GetPermissionProfileId(string functionKeyValue)
        {
            var criteria = new PermissionCriteria
            {
                FunctionKey = functionKeyValue
            };

            return await repository.GetAsync<int>(criteria);
        }
    }
}
