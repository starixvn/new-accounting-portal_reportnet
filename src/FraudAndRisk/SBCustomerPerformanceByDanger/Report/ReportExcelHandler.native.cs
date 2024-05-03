using AutoMapper;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

namespace SBCustomerPerformanceByDanger.Report
{
    public class ReportExcelHandler : IRequestHandler<CustomerPerformanceByDangerQueryExcelHttpParameter, ExcelPackage>
    {
        private readonly ReportService service;

        private readonly ReportExcelBuilder builder;

        private readonly IMapper mapper;

        public ReportExcelHandler(ReportService service, ReportExcelBuilder builder, IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.mapper = mapper;
        }

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
}