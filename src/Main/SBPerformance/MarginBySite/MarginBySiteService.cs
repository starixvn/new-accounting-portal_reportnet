using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBPerformance.MarginBySite
{
    public interface MarginBySiteService
    {
        Task<SummaryEntity<MarginBySiteItemModel>> Query(MarginBySiteHttpParameter parameter);
    }

    public class MarginBySiteServiceImp(IDynamicRepository repository, IMapper mapper) : MarginBySiteService
    {
        public async Task<SummaryEntity<MarginBySiteItemModel>> Query(MarginBySiteHttpParameter parameter)
        {
            var criteria = mapper.Map<MarginBySiteCriteria>(parameter);
            var summaryEntities = (await repository.FetchAsync<MarginBySiteEntity>(criteria)).CalculateSummaryEntity();

            var result = BindingItems(parameter.IsShowFullColumn, summaryEntities);
            return result;
        }

        private SummaryEntity<MarginBySiteItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<MarginBySiteEntity> entities)
        {
            var result = new SummaryEntity<MarginBySiteItemModel>();
            foreach (var entity in entities.Entities)
            {
                var item = BindingItem(isShowFullColumn, entity);
                result.Entities.Add(item);
            }

            var totalItem = BindingItem(isShowFullColumn, entities.Total);
            result.Total = totalItem;

            return result;
        }

        private MarginBySiteItemModel BindingItem(bool isShowFullColumn, MarginBySiteEntity entity)
        {
            var income = isShowFullColumn ? entity.RealIncome : 0;
            var turnover = isShowFullColumn ? entity.RealTurnover : 0;

            return new MarginBySiteItemModel
            {
                Id = entity.Id,
                Sport = entity.Sport,
                League = entity.League,
                Event = entity.Event,
                Match = entity.Match,
                BetType = entity.BetType,
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