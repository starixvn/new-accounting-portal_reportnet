using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.MarginBySite
{
    public class MarginBySiteHandler : IRequestHandler<MarginBySiteHttpParameter, SummaryEntity<MarginBySiteItemModel>>
    {
        private readonly MarginBySiteService service;

        public MarginBySiteHandler(MarginBySiteService service)
        {
            this.service = service;
        }

        public async Task<SummaryEntity<MarginBySiteItemModel>> Handle(MarginBySiteHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}