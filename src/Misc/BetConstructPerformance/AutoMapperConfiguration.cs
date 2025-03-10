namespace BetConstructPerformance;

using AutoMapper;
using Criteria;
using Models;
using Requests;

public static class AutoMapperConfiguration
{
    public static void InitializeBetConstructPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<SportRequest, SportCriteria>();
        cfg.CreateMap<SportItemModel, SportItemExcelModel>();
        cfg.CreateMap<SportItemModel, SportDetailItemExcelModel>();
        cfg.CreateMap<SportModel, SportDetailExcelModel>();
        cfg.CreateMap<SportModel, SportExcelModel>();
        cfg.CreateMap<SportExcelRequest, SportCriteria>();
    }
}