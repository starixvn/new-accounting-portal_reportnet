namespace SoccerPerformanceDetail.Services;

using Fanex.Data.Repository;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.ViewModel;

public interface SoccerPerformanceDetailService
{
    Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userid, BettypeViewPermission permission = BettypeViewPermission.All);
}


public class DetailReportService(IDynamicRepository repository) : SoccerPerformanceDetailService
{
    public async Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission = BettypeViewPermission.All)
    {
        var groupBetTypes = Enumerable.Empty<GroupBetTypes>();
      
        var criteria = new GetBetTypesCriteria(permission) { UserId = userId };
        
        var data = await repository.FetchAsync<BetTypeInfo>(criteria);

        if (data != null && data.Any())
        {
            groupBetTypes = data
                .GroupBy(g => new { g.GroupBettypeId, g.GroupBettypeName })
                .Select(x => new { Group = x.Key, BetTypeList = x.ToList() })
                .Select(x => new GroupBetTypes
                {
                    GroupId = x.Group.GroupBettypeId,
                    GroupName = x.Group.GroupBettypeName,
                    TotalDefaultCheckOfBetTypeList = x.BetTypeList.Where(y => y.IsDefault).Count(),
                    BetTypeList = x.BetTypeList.Select(y => new BetType
                    {
                        BetTypeId = y.BetType,
                        BetTypeName = y.TypeName,
                        IsDefaultCheck = y.IsDefault
                    })
                });
        }

        return groupBetTypes;
    }
}