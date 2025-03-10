namespace ActualMemberBet.Bettype;

using AutoMapper;
using Common;
using MediatR;
using Utilities.LinqDynamicSum;

public class Handler(ActualMemberBetService service, IMapper mapper)
    : IRequestHandler<HttpParameter, SummaryEntity<BetTypeActualMemberBet>>
{
    public async Task<SummaryEntity<BetTypeActualMemberBet>> Handle(HttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        return await service.Query<BetTypeActualMemberBet>(criteria);
    }
}