using ActualMemberBet.Common;
using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;

namespace ActualMemberBet.Product
{
    public class Handler : IRequestHandler<HttpParameter, SummaryEntity<ProductActualMemberBet>>
    {
        private readonly IMapper mapper;

        private readonly ActualMemberBetService service;

        public Handler(ActualMemberBetService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<ProductActualMemberBet>> Handle(HttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<Criteria>(request);
            return await service.Query<ProductActualMemberBet>(criteria);
        }
    }
}
