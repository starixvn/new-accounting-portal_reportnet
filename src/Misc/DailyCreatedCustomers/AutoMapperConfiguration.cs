namespace DailyCreatedCustomers;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeDailyCreatedCustomers(this IProfileExpression cfg)
    {
        cfg.CreateMap<SummaryItemModel, SummaryItemExcelModel>();
        cfg.CreateMap<DetailModel, DetailExcelModel>();
    }
}