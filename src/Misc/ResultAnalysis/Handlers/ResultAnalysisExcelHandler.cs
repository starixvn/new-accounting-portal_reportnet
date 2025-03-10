namespace ResultAnalysis.Handlers;

using AutoMapper;
using Criteria;
using MediatR;
using Models;
using OfficeOpenXml;
using Requests;
using Services;

public class ResultAnalysisExcelHandler(
    ResultAnalysisService service,
    ExcelBuilderService excelService,
    IMapper mapper)
    : IRequestHandler<ResultAnalysisExcelRequest, ExcelPackage>
{
    private const int Soccer = 1;

    public async Task<ExcelPackage> Handle(ResultAnalysisExcelRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ResultAnalysisCriteria>(request);
        var title = $"Result Analysis Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}";
        if (request.SportId == Soccer)
        {
            var data = await service.GetResultAnalysis(criteria);
            var excelModel = mapper.Map<ResultAnalysisExcelModel>(data);
            return excelService.Build(title, excelModel);
        }
        else
        {
            var data = await service.GetResultAnalysisOfQuarter(criteria);
            return excelService.BuildForQuarter(title, data);
        }
    }
}