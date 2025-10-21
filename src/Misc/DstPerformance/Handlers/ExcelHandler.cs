using AutoMapper;
using DstPerformance.Criteria;
using DstPerformance.Models;
using DstPerformance.Requests;
using DstPerformance.Services;
using MediatR;
using OfficeOpenXml;

namespace DstPerformance.Handlers;

public class ExcelHandler(
    DstPerformanceService service,
    ExcelBuilderService excelService,
    IMapper mapper) : IRequestHandler<SportExcelRequest, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(SportExcelRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ReportCriteria>(request);
        var data = await service.GetSports(criteria);
        var excelModel = mapper.Map<SportExcelModel>(data);
        return excelService.BuildSport(
            $"DST Performance Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}",
            excelModel);

    }
}