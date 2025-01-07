using Fanex.Data.Repository;
using SabaPromotion.Criteria;
using SabaPromotion.Models;

namespace SabaPromotion.Services;

public interface SabaPromotionService
{
    Task<IEnumerable<PromotionTypeModel>> GetPromotionTypes();

    Task<SabaPromotionModel> GetSabaPromotion(SabaPromotionCriteria criteria);
}

public class SabaPromotionServiceImp(IDynamicRepository repository) : SabaPromotionService
{
    public Task<IEnumerable<PromotionTypeModel>> GetPromotionTypes()
    {
        return repository.FetchAsync<PromotionTypeModel>(new PromotionTypeCriteria());
    }

    public async Task<SabaPromotionModel> GetSabaPromotion(SabaPromotionCriteria criteria)
    {
        var rawData = await repository.FetchAsync<SabaPromotionItem>(criteria);
        var result = new SabaPromotionModel
        {
            Items = rawData,
            Total = new SabaPromotionItem
            {
                HouseBonusExpense = rawData.Sum(item => item.HouseBonusExpense)
            }
        };
        return result;
    }
}