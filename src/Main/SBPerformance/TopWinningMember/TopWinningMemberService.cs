using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBPerformance.TopWinningMember;

public interface TopWinningMemberService
{
    Task<SummaryEntity<TopWinningMemberItemModel>> Query(TopWinningMemberHttpParameter parameter);
}

public class TopWinningMemberServiceImp(IDynamicRepository repository, IMapper mapper) : TopWinningMemberService
{
    public async Task<SummaryEntity<TopWinningMemberItemModel>> Query(TopWinningMemberHttpParameter parameter)
    {
        var criteria = mapper.Map<TopWinningMemberCriteria>(parameter);
        var summaryEntities = (await repository.FetchAsync<TopWinningMemberEntity>(criteria)).CalculateSummaryEntity();

        var result = BindingItems(parameter.IsShowFullColumn, summaryEntities);
        return result;
    }

    private SummaryEntity<TopWinningMemberItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<TopWinningMemberEntity> entities)
    {
        var result = new SummaryEntity<TopWinningMemberItemModel>();
        foreach (var item in entities.Entities.Select(entity => BindingItem(isShowFullColumn, entity)))
        {
            result.Entities.Add(item);
        }

        var totalItem = BindingItem(isShowFullColumn, entities.Total);
        result.Total = totalItem;

        return result;
    }

    private TopWinningMemberItemModel BindingItem(bool isShowFullColumn, TopWinningMemberEntity entity)
    {
        var income = isShowFullColumn ? entity.RealIncome : 0;
        var turnover = isShowFullColumn ? entity.RealTurnover : 0;

        return new TopWinningMemberItemModel
        {
            Id = entity.Id,
            League = entity.League,
            Event = entity.Event,
            Match = entity.Match,
            BetType = entity.BetType,
            Sport = entity.Sport,
            NoBetTypes = entity.NoBetTypes,
            NoMatches = entity.NoMatches,
            BetCount = entity.BetCount,
            RealIncome = income,
            RealTurnover = turnover,
            RealMargin = entity.RealMargin
        };
    }
}