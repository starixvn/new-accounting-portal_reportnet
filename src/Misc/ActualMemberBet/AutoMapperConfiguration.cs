namespace ActualMemberBet;

using AutoMapper;
using Bettype;
using Product;
using Utilities.LinqDynamicSum;
using Criteria = Product.Criteria;
using HttpParameter = Product.HttpParameter;
using HttpParameterExcel = Product.HttpParameterExcel;

public static class AutoMapperConfiguration
{
    public static void InitializeActualMemberBet(this IProfileExpression cfg)
    {

        cfg.CreateMap<HttpParameter, Criteria>();
        cfg.CreateMap<HttpParameterExcel, Criteria>();
        cfg.CreateMap<ProductActualMemberBet, ProductActualMemberBetExcel>();
        cfg.CreateMap<SummaryEntity<ProductActualMemberBet>, SummaryEntity<ProductActualMemberBetExcel>>();

        cfg.CreateMap<Bettype.HttpParameter, Bettype.Criteria>();
        cfg.CreateMap<Bettype.HttpParameterExcel, Bettype.Criteria>();
        cfg.CreateMap<BetTypeActualMemberBet, BetTypeActualMemberBetExcel>();
        cfg.CreateMap<SummaryEntity<BetTypeActualMemberBet>, SummaryEntity<BetTypeActualMemberBetExcel>>();
    }
}