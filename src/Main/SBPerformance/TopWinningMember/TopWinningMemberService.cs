using AutoMapper;
using Fanex.Data.Repository;
using Nap_Reports.Utilities.LinqDynamicSum;

namespace SBPerformance.TopWinningMember
{
    public interface TopWinningMemberService
    {
        Task<SummaryEntity<TopWinningMemberItemModel>> Query(TopWinningMemberHttpParameter parammeter);
    }

    public class TopWinningMemberServiceImp : TopWinningMemberService
    {
        private readonly IDynamicRepository repository;
        private readonly IMapper mapper;

        public TopWinningMemberServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<TopWinningMemberItemModel>> Query(TopWinningMemberHttpParameter parammeter)
        {
            var criteria = mapper.Map<TopWinningMemberCriteria>(parammeter);
            var summaryEntities = (await repository.FetchAsync<TopWinningMemberEntity>(criteria)).CalculateSummaryEntity();

            var result = BindingItems(parammeter.IsShowFullColumn, summaryEntities);
            return result;
        }

        private SummaryEntity<TopWinningMemberItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<TopWinningMemberEntity> entities)
        {
            var result = new SummaryEntity<TopWinningMemberItemModel>();
            foreach (var entity in entities.Entities)
            {
                var item = BindingItem(isShowFullColumn, entity);
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
}