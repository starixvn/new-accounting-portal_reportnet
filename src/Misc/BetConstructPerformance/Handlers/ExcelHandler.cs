namespace BetConstructPerformance.Handlers;

using AutoMapper;
using Criteria;
using MediatR;
using Models;
using OfficeOpenXml;
using Requests;
using Services;

public class ExcelHandler(
    BetConstructPerformanceService service,
    ExcelBuilderService excelService,
    IMapper mapper) : IRequestHandler<SportExcelRequest, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(SportExcelRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<SportCriteria>(request);
        var data = await service.GetSports(criteria);
        if (request.SportId.HasValue)
        {
            var excelModel = mapper.Map<SportDetailExcelModel>(data);
            return excelService.BuildSportDetail(request.SportName, excelModel);
        }
        else
        {
            var excelModel = mapper.Map<SportExcelModel>(data);
            return excelService.BuildSport($"BetConstruct Performance Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}", excelModel);
        }

    }
}