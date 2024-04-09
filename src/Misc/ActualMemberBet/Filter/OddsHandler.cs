using Fanex.Data.Repository;
using MediatR;

namespace ActualMemberBet.Filter
{
    public class OddsHandler : IRequestHandler<OddsHttpParameter, IEnumerable<Odds>>
    {
        private readonly IDynamicRepository repository;

        public OddsHandler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Odds>> Handle(OddsHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = new OddsCriteria();
            return await repository.FetchAsync<Odds>(criteria);
        }
    }
}
