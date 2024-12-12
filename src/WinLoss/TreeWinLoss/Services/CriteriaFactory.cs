using Fanex.Data.Repository;
using TreeWinLoss.Criteria;

namespace TreeWinLoss.Services
{
    public interface CriteriaFactory
    {
        CriteriaBase CreateCriteria(TreeWinLossByLevelCriteria criteria);
    }

    public class CriteriaFactoryImp : CriteriaFactory
    {
        public CriteriaBase CreateCriteria(TreeWinLossByLevelCriteria criteria)
        {
            return criteria.RoleId switch
            {
                5 => new TreeWinLossByComCriteria
                {
                    Currency = criteria.Currency,
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    ProductSelection = criteria.ProductSelection,
                    Products = criteria.Products,
                    IsNormalMode = criteria.IsNormalMode,
                    SportType = criteria.SportType
                },
                4 => new TreeWinLossBySuperCriteria
                {
                    SRecommend = criteria.SRecommend,
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    ProductSelection = criteria.ProductSelection,
                    Products = criteria.Products,
                    IsNormalMode = criteria.IsNormalMode,
                    SportType = criteria.SportType
                },
                3 => new TreeWinLossByMasterCriteria
                {
                    MRecommend = criteria.MRecommend,
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    ProductSelection = criteria.ProductSelection,
                    Products = criteria.Products,
                    IsNormalMode = criteria.IsNormalMode,
                    SportType = criteria.SportType
                },
                _ => new TreeWinLossByAgentCriteria
                {
                    Recommend = criteria.Recommend,
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    ProductSelection = criteria.ProductSelection,
                    Products = criteria.Products,
                    IsNormalMode = criteria.IsNormalMode,
                    SportType = criteria.SportType
                }
            };
        }
    }
}
