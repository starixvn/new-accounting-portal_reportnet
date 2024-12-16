using Fanex.Data.Repository;

namespace Utilities.PermissionFilter;

public interface PermissionFilterService
{
    Task<int> GetPermissionProfileId(string functionKeyValue);
}

public class PermissionFilterServiceImp(IDynamicRepository repository) : PermissionFilterService
{
    public async Task<int> GetPermissionProfileId(string functionKeyValue)
    {
        var criteria = new PermissionCriteria
        {
            FunctionKey = functionKeyValue
        };

        return await repository.GetAsync<int>(criteria);
    }
}