using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBPerformance.MarginBySport
{
    public interface MarginBySportService
    {
        Task<SummaryEntity<MarginBySportItemModel>> Query(MarginBySportHttpParameter parameter);
    }

    public class MarginBySportServiceImp : MarginBySportService
    {
        private readonly IDynamicRepository repository;
        private readonly IMapper mapper;

        public MarginBySportServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<MarginBySportItemModel>> Query(MarginBySportHttpParameter parameter)
        {
            var criteria = mapper.Map<MarginBySportCriteria>(parameter);
            var summaryEntities = (await repository.FetchAsync<MarginBySportEntity>(criteria)).CalculateSummaryEntity();

            var result = BindingItems(parameter.IsShowFullColumn, summaryEntities);
            return result;
        }

        private SummaryEntity<MarginBySportItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<MarginBySportEntity> entities)
        {
            var result = new SummaryEntity<MarginBySportItemModel>();
            foreach (var entity in entities.Entities)
            {
                var item = BindingItem(isShowFullColumn, entity);
                result.Entities.Add(item);
            }

            var totalItem = BindingItem(isShowFullColumn, entities.Total);
            result.Total = totalItem;

            return result;
        }

        private MarginBySportItemModel BindingItem(bool isShowFullColumn, MarginBySportEntity entity)
        {
            var income = isShowFullColumn ? entity.RealIncome : 0;
            var turnover = isShowFullColumn ? entity.RealTurnover : 0;

            return new MarginBySportItemModel
            {
                Id = entity.Id,
                Site = entity.Site,
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