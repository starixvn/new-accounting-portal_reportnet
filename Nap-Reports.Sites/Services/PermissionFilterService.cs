using Fanex.Data.Repository;
using Nap_Reports.Sites.Criteria;

namespace Nap_Reports.Sites.Services
{
    public interface PermissionFilterService
    {
        int GetPermissionProfileId(string functionKeyValue);
    }

    public class PermissionFilterServiceImp : PermissionFilterService
    {
        private readonly IDynamicRepository repository;

        public PermissionFilterServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public int GetPermissionProfileId(string functionKeyValue)
        {
            var criteria = new PermissionCriteria
            {
                FunctionKey = functionKeyValue
            };

            return repository.Get<int>(criteria);
        }
    }
}
