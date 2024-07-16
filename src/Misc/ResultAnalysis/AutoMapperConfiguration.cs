using AutoMapper;
using ResultAnalysis.Criteria;
using ResultAnalysis.Models;
using ResultAnalysis.Requests;

namespace ResultAnalysis
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeResultAnalysis(this IProfileExpression cfg)
        {
            cfg.CreateMap<LeagueRequest, LeagueCriteria>();
            cfg.CreateMap<ResultAnalysisRequest, ResultAnalysisCriteria>();
            cfg.CreateMap<ResultAnalysisExcelRequest, ResultAnalysisCriteria>();
            cfg.CreateMap<ResultAnalysisSummaryExcelModel, ResultAnalysisSummaryModel>();
            cfg.CreateMap<DetailedResultAnalysisExcelModel, DetailedResultAnalysisModel>();
            cfg.CreateMap<IEnumerable<DetailedResultAnalysisExcelModel>, IEnumerable<DetailedResultAnalysisModel>>();
            cfg.CreateMap<ResultAnalysisExcelModel, ResultAnalysisModel>();
        }
    }
}
