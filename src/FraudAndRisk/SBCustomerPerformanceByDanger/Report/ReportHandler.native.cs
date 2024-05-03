using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;

namespace SBCustomerPerformanceByDanger.Report
{
    public class ReportHandler : IRequestHandler<CustomerPerformanceByDangerQueryHttpParameter, SummaryEntity<CustomerPerformanceByDangerModel>>
    {
        private readonly ReportService service;

        private readonly IMapper mapper;

        public ReportHandler(ReportService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<CustomerPerformanceByDangerModel>> Handle(
            CustomerPerformanceByDangerQueryHttpParameter request,
            CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<CustomerPerformanceByDangerQueryCriteria>(request);
            return await service.GetCustomerPerformanceByDanger(criteria);
        }
    }
}