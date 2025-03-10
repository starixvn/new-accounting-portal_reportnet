namespace ActualMemberBet.Product;

using AutoMapper;
using Common;
using MediatR;
using Utilities.LinqDynamicSum;

public class Handler(ActualMemberBetService service, IMapper mapper)
    : IRequestHandler<HttpParameter, SummaryEntity<ProductActualMemberBet>>
{
    public async Task<SummaryEntity<ProductActualMemberBet>> Handle(HttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        return await service.Query<ProductActualMemberBet>(criteria);
    }
}