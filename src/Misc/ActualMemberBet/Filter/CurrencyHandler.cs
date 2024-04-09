using Fanex.Data.Repository;
using MediatR;

namespace ActualMemberBet.Filter
{
    public class CurrencyHandler : IRequestHandler<CurrencyHttpParameter, IEnumerable<Currency>>
    {
        private readonly IDynamicRepository repository;

        public CurrencyHandler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Currency>> Handle(CurrencyHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = new CurrencyCriteria();
            return await repository.FetchAsync<Currency>(criteria);
        }
    }
}
