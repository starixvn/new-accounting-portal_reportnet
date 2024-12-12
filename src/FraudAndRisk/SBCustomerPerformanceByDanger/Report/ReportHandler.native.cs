using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;

namespace SBCustomerPerformanceByDanger.Report
{
    public class ReportHandler(ReportService service, IMapper mapper)
        : IRequestHandler<CustomerPerformanceByDangerQueryHttpParameter,
            SummaryEntity<CustomerPerformanceByDangerModel>>
    {
        public async Task<SummaryEntity<CustomerPerformanceByDangerModel>> Handle(
            CustomerPerformanceByDangerQueryHttpParameter request,
            CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<CustomerPerformanceByDangerQueryCriteria>(request);
            return await service.GetCustomerPerformanceByDanger(criteria);
        }
    }
}