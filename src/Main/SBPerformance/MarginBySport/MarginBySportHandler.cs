using MediatR;
using Nap_Reports.Utilities.LinqDynamicSum;

namespace SBPerformance.MarginBySport
{
    public class MarginBySportHandler : IRequestHandler<MarginBySportHttpParameter, SummaryEntity<MarginBySportItemModel>>
    {
        private readonly MarginBySportService service;

        public MarginBySportHandler(MarginBySportService service)
        {
            this.service = service;
        }

        public async Task<SummaryEntity<MarginBySportItemModel>> Handle(MarginBySportHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}