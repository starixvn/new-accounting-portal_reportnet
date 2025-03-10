namespace ResultAnalysis;

using AutoMapper;
using Criteria;
using Models;
using Requests;

public static class AutoMapperConfiguration
{
    public static void InitializeResultAnalysis(this IProfileExpression cfg)
    {
        cfg.CreateMap<LeagueRequest, LeagueCriteria>();
        cfg.CreateMap<ResultAnalysisRequest, ResultAnalysisCriteria>();
        cfg.CreateMap<ResultAnalysisExcelRequest, ResultAnalysisCriteria>();

        cfg.CreateMap<ResultAnalysisSummaryModel, ResultAnalysisSummaryExcelModel>();
        cfg.CreateMap<DetailedResultAnalysisModel, DetailedResultAnalysisExcelModel>();
        cfg.CreateMap<ResultAnalysisModel, ResultAnalysisExcelModel>();

        cfg.CreateMap<ResultAnalysisOfQuarterRequest, ResultAnalysisCriteria>();
        cfg.CreateMap<BaseResultAnalysisSummaryModel, ResultAnalysisSummaryOfQuarterExcelModel>();
        cfg.CreateMap<DetailedResultAnalysisOfQuarterModel, DetailedResultAnalysisOfHomeQuarterExcelModel>();
        cfg.CreateMap<DetailedResultAnalysisOfQuarterModel, DetailedResultAnalysisOfAwayQuarterExcelModel>();
    }
}