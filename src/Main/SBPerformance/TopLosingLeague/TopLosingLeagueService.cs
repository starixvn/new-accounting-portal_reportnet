using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBPerformance.TopLosingLeague
{
    public interface TopLosingLeagueService
    {
        Task<SummaryEntity<TopLosingLeagueItemModel>> Query(TopLosingLeagueHttpParameter parameter);
    }

    public class TopLosingLeagueServiceImp : TopLosingLeagueService
    {
        private readonly IDynamicRepository repository;
        private readonly IMapper mapper;

        public TopLosingLeagueServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<TopLosingLeagueItemModel>> Query(TopLosingLeagueHttpParameter parameter)
        {
            var criteria = mapper.Map<TopLosingLeagueCriteria>(parameter);
            var summaryEntities = (await repository.FetchAsync<TopLosingLeagueEntity>(criteria)).CalculateSummaryEntity();

            var result = BindingItems(parameter.IsShowFullColumn, summaryEntities);
            return result;
        }

        private SummaryEntity<TopLosingLeagueItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<TopLosingLeagueEntity> entities)
        {
            var result = new SummaryEntity<TopLosingLeagueItemModel>();
            foreach (var entity in entities.Entities)
            {
                var item = BindingItem(isShowFullColumn, entity);
                result.Entities.Add(item);
            }

            var totalItem = BindingItem(isShowFullColumn, entities.Total);
            result.Total = totalItem;

            return result;
        }

        private TopLosingLeagueItemModel BindingItem(bool isShowFullColumn, TopLosingLeagueEntity entity)
        {
            var income = isShowFullColumn ? entity.RealIncome : 0;
            var turnover = isShowFullColumn ? entity.RealTurnover : 0;

            return new TopLosingLeagueItemModel
            {
                Id = entity.Id,
                Site = entity.Site,
                League = entity.League,
                Event = entity.Event,
                Match = entity.Match,
                BetType = entity.BetType,
                Username = entity.Username,
                NoBetTypes = entity.NoBetTypes,
                NoMatches = entity.NoMatches,
                ActiveMembers = entity.ActiveMembers,
                BetCount = entity.BetCount,
                RealIncome = income,
                RealTurnover = turnover,
                RealMargin = entity.RealMargin
            };
        }
    }
}