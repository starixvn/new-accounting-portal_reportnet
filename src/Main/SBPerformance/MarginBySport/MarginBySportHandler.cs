using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.MarginBySport
{
    public class MarginBySportHandler(MarginBySportService service)
        : IRequestHandler<MarginBySportHttpParameter, SummaryEntity<MarginBySportItemModel>>
    {
        public async Task<SummaryEntity<MarginBySportItemModel>> Handle(MarginBySportHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}