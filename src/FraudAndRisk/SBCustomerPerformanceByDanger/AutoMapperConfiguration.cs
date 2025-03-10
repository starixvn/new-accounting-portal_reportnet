namespace SBCustomerPerformanceByDanger;

using AutoMapper;
using Filter;
using Report;
using Utilities.LinqDynamicSum;

public static class AutoMapperConfiguration
{
    public static void InitializeSBCustomerPerformanceByDanger(this IProfileExpression cfg)
    {
        cfg.CreateMap<SportQueryHttpParameter, SportQueryCriteria>();
        cfg.CreateMap<LeagueGroupQueryHttpParameter, LeagueGroupQueryCriteria>();
        cfg.CreateMap<LeagueQueryHttpParameter, LeagueQueryCriteria>();
        cfg.CreateMap<DivisionQueryHttpParameter, DivisionQueryCriteria>();
        cfg.CreateMap<CustomerPerformanceByDangerQueryHttpParameter, CustomerPerformanceByDangerQueryCriteria>();
        cfg.CreateMap<CustomerPerformanceByDangerQueryExcelHttpParameter, CustomerPerformanceByDangerQueryCriteria>();
        cfg.CreateMap<CustomerPerformanceByDangerModel, CustomerPerformanceByDangerExcelModel>();
        cfg.CreateMap<SummaryEntity<CustomerPerformanceByDangerModel>, SummaryEntity<CustomerPerformanceByDangerExcelModel>>();
    }
}