using AutoMapper;
using Utilities.LinqDynamicSum;

namespace ActualMemberBet
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeActualMemberBet(this IProfileExpression cfg)
        {

            cfg.CreateMap<Product.HttpParameter, Product.Criteria>();
            cfg.CreateMap<Product.HttpParameterExcel, Product.Criteria>();
            cfg.CreateMap<Product.ProductActualMemberBet, Product.ProductActualMemberBetExcel>();
            cfg.CreateMap<SummaryEntity<Product.ProductActualMemberBet>, SummaryEntity<Product.ProductActualMemberBetExcel>>();

            cfg.CreateMap<Bettype.HttpParameter, Bettype.Criteria>();
            cfg.CreateMap<Bettype.HttpParameterExcel, Bettype.Criteria>();
            cfg.CreateMap<Bettype.BetTypeActualMemberBet, Bettype.BetTypeActualMemberBetExcel>();
            cfg.CreateMap<SummaryEntity<Bettype.BetTypeActualMemberBet>, SummaryEntity<Bettype.BetTypeActualMemberBetExcel>>();
        }
    }
}
