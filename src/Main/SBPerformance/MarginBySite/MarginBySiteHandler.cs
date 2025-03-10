namespace SBPerformance.MarginBySite;

using MediatR;
using Utilities.LinqDynamicSum;

public class MarginBySiteHandler(MarginBySiteService service)
    : IRequestHandler<MarginBySiteHttpParameter, SummaryEntity<MarginBySiteItemModel>>
{
    public async Task<SummaryEntity<MarginBySiteItemModel>> Handle(MarginBySiteHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.Query(request);
    }
}