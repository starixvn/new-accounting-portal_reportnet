using Fanex.Data.Repository;
using MediatR;

namespace ActualMemberBet.Filter;

public class CurrencyHandler(IDynamicRepository repository)
    : IRequestHandler<CurrencyHttpParameter, IEnumerable<Currency>>
{
    public async Task<IEnumerable<Currency>> Handle(CurrencyHttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = new CurrencyCriteria();
        return await repository.FetchAsync<Currency>(criteria);
    }
}