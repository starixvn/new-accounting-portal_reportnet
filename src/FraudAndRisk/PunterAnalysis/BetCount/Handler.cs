namespace PunterAnalysis.BetCount;

using AutoMapper;
using MediatR;
using Model;
using Utilities.LinqDynamicSum;

public class Handler(BetCountService betCountService, IMapper mapper)
    : IRequestHandler<HttpParameterJson, SummaryEntity<BetCountModel>>
{
    public async Task<SummaryEntity<BetCountModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        return await betCountService.Query(criteria);
    }
}