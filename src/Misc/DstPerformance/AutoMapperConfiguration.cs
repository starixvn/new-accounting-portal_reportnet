using AutoMapper;
using DstPerformance.Criteria;
using DstPerformance.Models;
using DstPerformance.Requests;

namespace DstPerformance;

public static class AutoMapperConfiguration
{
    public static void InitializeDstPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<SportRequest, ReportCriteria>();
        cfg.CreateMap<SportItemModel, SportItemExcelModel>();
        cfg.CreateMap<SportModel, SportExcelModel>();
        cfg.CreateMap<SportExcelRequest, ReportCriteria>();
    }
}