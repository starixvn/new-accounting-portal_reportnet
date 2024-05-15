using AutoMapper;
using MediatR;
using PunterAnalysis.BetCount.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetCount
{
    public class Handler : IRequestHandler<HttpParameterJson, SummaryEntity<BetCountModel>>
    {
        private readonly BetCountService betCountService;

        private readonly IMapper mapper;

        public Handler(BetCountService betCountService, IMapper mapper)
        {
            this.betCountService = betCountService;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<BetCountModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<Criteria>(request);
            return await betCountService.Query(criteria);
        }
    }
}