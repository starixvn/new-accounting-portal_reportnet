using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.Main
{
    public class MainHandler(MainService service) : IRequestHandler<HttpParameter, SummaryEntity<ItemModel>>
    {
        public async Task<SummaryEntity<ItemModel>> Handle(HttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}