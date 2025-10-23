using AutoMapper;
using DstPerformance.Criteria;
using DstPerformance.Models;
using DstPerformance.Requests;

namespace DstPerformance;

public static class AutoMapperConfiguration
{
    public static void InitializeDstPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<ReportRequest, ReportCriteria>();
        cfg.CreateMap<ReportItemModel, ReportItemExcelModel>();
        cfg.CreateMap<ReportModel, ReportExcelModel>();
        cfg.CreateMap<ReportExcelRequest, ReportCriteria>();
    }
}