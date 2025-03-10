namespace SBPerformance.MarginBySport;

using MediatR;
using Utilities.LinqDynamicSum;

public class MarginBySportHandler(MarginBySportService service)
    : IRequestHandler<MarginBySportHttpParameter, SummaryEntity<MarginBySportItemModel>>
{
    public async Task<SummaryEntity<MarginBySportItemModel>> Handle(MarginBySportHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.Query(request);
    }
}