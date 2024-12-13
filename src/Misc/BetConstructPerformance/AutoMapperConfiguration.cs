using AutoMapper;
using BetConstructPerformance.Criteria;
using BetConstructPerformance.Models;
using BetConstructPerformance.Requests;

namespace BetConstructPerformance
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeBetConstructPerformance(this IProfileExpression cfg)
        {
            cfg.CreateMap<SportRequest, SportCriteria>();
            cfg.CreateMap<SportItemModel, SportItemExcelModel>();
            cfg.CreateMap<SportModel, SportDetailExcelModel>();
            cfg.CreateMap<SportModel, SportExcelModel>();
            cfg.CreateMap<SportExcelRequest, SportCriteria>();
        }
    }
}
