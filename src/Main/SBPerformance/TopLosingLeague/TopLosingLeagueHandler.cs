using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.TopLosingLeague
{
    public class TopLosingLeagueHandler(TopLosingLeagueService service)
        : IRequestHandler<TopLosingLeagueHttpParameter, SummaryEntity<TopLosingLeagueItemModel>>
    {
        public async Task<SummaryEntity<TopLosingLeagueItemModel>> Handle(TopLosingLeagueHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}