using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.TopLosingLeague
{
    public class TopLosingLeagueHandler : IRequestHandler<TopLosingLeagueHttpParameter, SummaryEntity<TopLosingLeagueItemModel>>
    {
        private readonly TopLosingLeagueService service;

        public TopLosingLeagueHandler(TopLosingLeagueService service)
        {
            this.service = service;
        }

        public async Task<SummaryEntity<TopLosingLeagueItemModel>> Handle(TopLosingLeagueHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}