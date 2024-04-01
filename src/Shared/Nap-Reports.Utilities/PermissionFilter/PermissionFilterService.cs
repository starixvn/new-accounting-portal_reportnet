using Fanex.Data.Repository;

namespace Nap_Reports.Utilities.PermissionFilter
{
    public interface PermissionFilterService
    {
        Task<int> GetPermissionProfileId(string functionKeyValue);
    }

    public class PermissionFilterServiceImp : PermissionFilterService
    {
        private readonly IDynamicRepository repository;

        public PermissionFilterServiceImp(IDynamicRepository repository)
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
