using ActualMemberBet.Common;
using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;

namespace ActualMemberBet.Bettype
{
    public class Handler : IRequestHandler<HttpParameter, SummaryEntity<BetTypeActualMemberBet>>
    {
        private readonly ActualMemberBetService service;
        private readonly IMapper mapper;

        public Handler(ActualMemberBetService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<BetTypeActualMemberBet>> Handle(HttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<Criteria>(request);
            return await service.Query<BetTypeActualMemberBet>(criteria);
        }
    }
}
