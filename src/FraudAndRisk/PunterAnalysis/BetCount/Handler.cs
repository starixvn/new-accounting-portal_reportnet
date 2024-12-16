using AutoMapper;
using MediatR;
using PunterAnalysis.BetCount.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetCount;

public class Handler(BetCountService betCountService, IMapper mapper)
    : IRequestHandler<HttpParameterJson, SummaryEntity<BetCountModel>>
{
    public async Task<SummaryEntity<BetCountModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        return await betCountService.Query(criteria);
    }
}