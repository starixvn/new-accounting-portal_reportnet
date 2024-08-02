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
            switch (criteria.RoleId)
            {
                case 5:
                    return new TreeWinLossByComCriteria
                    {
                        Currency = criteria.Currency,
                        FromDate = criteria.FromDate,
                        ToDate = criteria.ToDate,
                        ProductSelection = criteria.ProductSelection,
                        Products = criteria.Products,
                        IsNormalMode = criteria.IsNormalMode,
                        SportType = criteria.SportType
                    };

                case 4:
                    return new TreeWinLossBySuperCriteria
                    {
                        SRecommend = criteria.SRecommend,
                        FromDate = criteria.FromDate,
                        ToDate = criteria.ToDate,
                        ProductSelection = criteria.ProductSelection,
                        Products = criteria.Products,
                        IsNormalMode = criteria.IsNormalMode,
                        SportType = criteria.SportType
                    };

                case 3:
                    return new TreeWinLossByMasterCriteria
                        {
                            MRecommend = criteria.MRecommend,
                            FromDate = criteria.FromDate,
                            ToDate = criteria.ToDate,
                            ProductSelection = criteria.ProductSelection,
                            Products = criteria.Products,
                            IsNormalMode = criteria.IsNormalMode,
                            SportType = criteria.SportType
                        };

                default:
                    return new TreeWinLossByAgentCriteria
                    {
                        Recommend = criteria.Recommend,
                        FromDate = criteria.FromDate,
                        ToDate = criteria.ToDate,
                        ProductSelection = criteria.ProductSelection,
                        Products = criteria.Products,
                        IsNormalMode = criteria.IsNormalMode,
                        SportType = criteria.SportType
                    };
            }
        }
    }
}
