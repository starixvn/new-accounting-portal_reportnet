namespace SBCustomerPerformanceByDanger.Report;

using AutoMapper;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

public class ReportExcelHandler(ReportService service, ReportExcelBuilder builder, IMapper mapper)
    : IRequestHandler<CustomerPerformanceByDangerQueryExcelHttpParameter, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(
        CustomerPerformanceByDangerQueryExcelHttpParameter request,
        CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<CustomerPerformanceByDangerQueryCriteria>(request);
        var reportData = await service.GetCustomerPerformanceByDanger(criteria);
        var excelModel = mapper.Map<SummaryEntity<CustomerPerformanceByDangerExcelModel>>(reportData);
        var excelPackage = builder.Build(request.Title, excelModel);
        return excelPackage;
    }
}