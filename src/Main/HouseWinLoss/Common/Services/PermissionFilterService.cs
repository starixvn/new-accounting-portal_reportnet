namespace HouseWinLoss.Common.Services;

using Criteria;
using Fanex.Data.Repository;

public interface PermissionFilterService1
{
    Task<int> GetPermissionProfileId(string functionKeyValue);
}

public class PermissionFilterServiceImp1(IDynamicRepository repository) : PermissionFilterService1
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