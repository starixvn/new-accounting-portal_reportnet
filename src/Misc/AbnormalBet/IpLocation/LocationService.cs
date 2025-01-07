using AbnormalBet.Criteria;
using AbnormalBet.Model;
using Fanex.Data.Repository;

namespace AbnormalBet.IpLocation;

public interface LocationService
{
    Task<LocationModel> QueryIP(string ip);
}

public class LocationServiceImp(IDynamicRepository repository) : LocationService
{
    public async Task<LocationModel> QueryIP(string ip)
    {
        ip = string.IsNullOrEmpty(ip) ? string.Empty : ip.Trim();
        var criteria = new GetIPInfoCriteria { IP = ip };
        return await repository.GetAsync<LocationModel>(criteria);
    }
}