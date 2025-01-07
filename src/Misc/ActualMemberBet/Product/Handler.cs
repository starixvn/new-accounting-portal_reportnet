using ActualMemberBet.Common;
using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;

namespace ActualMemberBet.Product;

public class Handler(ActualMemberBetService service, IMapper mapper)
    : IRequestHandler<HttpParameter, SummaryEntity<ProductActualMemberBet>>
{
    public async Task<SummaryEntity<ProductActualMemberBet>> Handle(HttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        return await service.Query<ProductActualMemberBet>(criteria);
    }
}