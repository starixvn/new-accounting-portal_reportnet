using ResultAnalysis.Services;

namespace Reports.Sites.DependencyInjection;

public static class ResultAnalysisInjection
{
    public static void RegisterResultAnalysisServices(this IServiceCollection service)
    {
        service.AddTransient<ResultAnalysisService, ResultAnalysisServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}