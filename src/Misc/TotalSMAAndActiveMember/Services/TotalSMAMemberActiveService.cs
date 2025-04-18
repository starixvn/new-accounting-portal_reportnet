namespace TotalSMAAndActiveMember.Services;

using Constant;
using Criteria;
using Entities;
using Fanex.Data.Repository;
using Model;

public interface TotalSMAMemberActiveService
{
    Task<IEnumerable<SiteModel>> GetSites();

    Task<IList<SMASummaryModel>> GetReport(TotalSMAMemberActiveCriteria criteria);

    Task<List<GroupStatusDetailModel>> GetStatusDetails(StatusDetailsCriteria criteria);
}

public class TotalSMAMemberActiveServiceImp(IDynamicRepository repository) : TotalSMAMemberActiveService
{
    public async Task<IList<SMASummaryModel>> GetReport(TotalSMAMemberActiveCriteria criteria)
    {
        var report = new List<SMASummaryModel>();

        var (accountStatusItems, activeDetails) = await repository.FetchMultipleAsync<AccountStatusItem, ActiveDetailItem>(criteria);

        var totalSMA = accountStatusItems.FirstOrDefault();

        if (totalSMA == null || activeDetails == null || !activeDetails.Any())
        {
            return report;
        }

        report =
        [
            new SMASummaryModel
            {
                Name = "New Created",
                Super = totalSMA.NewCreatedSuper,
                Master = totalSMA.NewCreatedMaster,
                Agent = totalSMA.NewCreatedAgent
            },

            new SMASummaryModel
            {
                Name = "Active",
                Super = totalSMA.ActiveSuper,
                Master = totalSMA.ActiveMaster,
                Agent = totalSMA.ActiveAgent
            },

            new SMASummaryModel
            {

                Name = "Suspended",
                Super = totalSMA.SuspendedSuper,
                Master = totalSMA.SuspendedMaster,
                Agent = totalSMA.SuspendedAgent
            },

            new SMASummaryModel
            {
                Name = "Closed",
                Super = totalSMA.ClosedSuper,
                Master = totalSMA.ClosedMaster,
                Agent = totalSMA.ClosedAgent
            }

        ];
        report.AddRange(activeDetails.Select(BuildSMASummary));

        return report;
    }

    public async Task<IEnumerable<SiteModel>> GetSites()
    {
        var criteria = new SiteCriteria();

        var result = await repository.FetchAsync<SiteModel>(criteria);

        return result;
    }
        
    public async Task<List<GroupStatusDetailModel>> GetStatusDetails(StatusDetailsCriteria criteria)
    {
        var status = (StatusConstant)criteria.Status;

        var report = new List<GroupStatusDetailModel>();
        criteria.PageIndex++;
        var rawData = await repository.FetchAsync<StatusDetailItem>(criteria);

        var totalRow = criteria.TotalRow;


        if (totalRow <= 0) return report;
        criteria.TotalRow = totalRow;
        report = GetStatusDetails(rawData, status);

        return report;
    }

    private List<GroupStatusDetailModel> GetStatusDetails(IEnumerable<StatusDetailItem> rawData, StatusConstant status)
    {
        var report = new List<GroupStatusDetailModel>();

        var groupHeadlines = rawData.GroupBy(data => data.GroupHeadline);

        foreach (var groupHeadline in groupHeadlines)
        {
            var groupStatusDetail = new GroupStatusDetailModel
            {
                Name = groupHeadline.Key,
                ChildItems = []
            };

            foreach (var item in groupHeadline)
            {
                var statusDetail = BuildStatusDetail(item, status);
                groupStatusDetail.ChildItems.Add(statusDetail);
            }

            report.Add(groupStatusDetail);
        }

        return report;
    }

    private SMASummaryModel BuildSMASummary(ActiveDetailItem rawData)
    {
        var smaArray = rawData.SMA.Split('/', StringSplitOptions.RemoveEmptyEntries);

        return new SMASummaryModel
        {
            Id = rawData.Id,
            Name = rawData.SiteName,
            Super = Convert.ToInt32(smaArray[0]),
            Master = Convert.ToInt32(smaArray[1]),
            Agent = Convert.ToInt32(smaArray[2])
        };
    }

    private StatusDetailModel BuildStatusDetail(StatusDetailItem rawData, StatusConstant status)
    {
        var statusDetail = new StatusDetailModel
        {
            Date = rawData.Date,
            Username = rawData.Username,
            Status = rawData.Status,
            Site = rawData.Site,
            Currency = rawData.Currency,
            Position = rawData.Position
        };

        if (status != StatusConstant.NewCreated)
        {
            return statusDetail;
        }

        statusDetail.CreatedTime = rawData.CreatedTime;
        statusDetail.Creator = rawData.Creator;

        return statusDetail;
    }
}