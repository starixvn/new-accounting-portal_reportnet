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
    IMapper mapper) : IRequestHandler<ReportExcelRequest, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(ReportExcelRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ReportCriteria>(request);
        var data = await service.GetData(criteria);
        data.Items.ToList().ForEach(item => item.Level = request.Level);
        var excelModel = mapper.Map<ReportExcelModel>(data);
        return excelService.BuildExcel(
            $"DST Performance Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}",
            request.Level,
            excelModel);

    }
}