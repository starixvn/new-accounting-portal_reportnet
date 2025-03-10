namespace AbnormalBet.Report;

using Criteria;
using Fanex.Data.Repository;
using IpLocation;
using Model;

public interface ReportService
{
    Task<MemberModel> QueryMember(MemberQueryCriteria criteria);

    Task<LicenseeModel> QueryLicensee(LicenseeQueryCriteria criteria);
}

public class ReportServiceImp(IDynamicRepository repository, LocationService locationService)
    : ReportService
{
    public async Task<LocationModel> QueryIP(string ip)
    {
        ip = string.IsNullOrEmpty(ip) ? string.Empty : ip.Trim();
        var criteria = new GetIPInfoCriteria { IP = ip };
        return await repository.GetAsync<LocationModel>(criteria);
    }

    public async Task<LicenseeModel> QueryLicensee(LicenseeQueryCriteria criteria)
    {
        var rawData = await repository.FetchMultipleAsync<LicenseeItemModel, int>(criteria);

        return new LicenseeModel
        {
            Licensees = rawData.Item1,
            TotalItems = rawData.Item2.FirstOrDefault()
        };
    }

    public async Task<MemberModel> QueryMember(MemberQueryCriteria criteria)
    {
        var rawData = await repository.FetchMultipleAsync<MemberItemModel, int>(criteria);

        foreach (var item in rawData.Item1)
        {
            item.Location = await GetLocationFromIp(item.IP);
        }

        return new MemberModel
        {
            Members = rawData.Item1,
            TotalItems = rawData.Item2.FirstOrDefault()
        };
    }

    private async Task<string> GetLocationFromIp(string ip)
    {
        if (string.IsNullOrEmpty(ip))
        {
            return string.Empty;
        }

        var locations = new List<string>();
        var ips = ip.Split(',').ToList();
        const byte locationMinLength = 2;
        foreach (var i in ips)
        {
            var location = await locationService.QueryIP(i);
            var list = new List<string>
            {
                location.CountryLong,
                location.IpRegion,
                location.IpCity
            };

            locations.Add(string.Join(", ", list.Where(s => s.Length > locationMinLength)));
        }

        return string.Join("; ", locations);
    }
}