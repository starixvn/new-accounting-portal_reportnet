using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBPerformance.Main
{
    public interface MainService
    {
        Task<SummaryEntity<ItemModel>> Query(HttpParameter parameter);
    }

    public class MainServiceImp(IDynamicRepository repository, IMapper mapper) : MainService
    {
        public async Task<SummaryEntity<ItemModel>> Query(HttpParameter parameter)
        {
            var criteria = mapper.Map<Criteria>(parameter);
            var summaryEntities = (await repository.FetchAsync<Entity>(criteria)).CalculateSummaryEntity();

            var result = BindingItems(parameter.IsShowFullColumn, summaryEntities);
            return result;
        }

        private SummaryEntity<ItemModel> BindingItems(bool isShowFullColumn, SummaryEntity<Entity> entities)
        {
            var result = new SummaryEntity<ItemModel>();
            foreach (var item in entities.Entities.Select(entity => BindingItem(isShowFullColumn, entity)))
            {
                result.Entities.Add(item);
            }

            var totalItem = BindingItem(isShowFullColumn, entities.Total);
            result.Total = totalItem;

            return result;
        }

        private ItemModel BindingItem(bool isShowFullColumn, Entity entity)
        {
            var income = isShowFullColumn ? entity.RealIncome : 0;
            var turnover = isShowFullColumn ? entity.RealTurnover : 0;

            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name,
                BetCount = entity.BetCount,
                RealIncome = income,
                RealTurnover = turnover,
                RealMargin = entity.RealMargin
            };
        }
    }
}