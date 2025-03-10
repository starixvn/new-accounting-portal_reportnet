namespace ActualMemberBet.Filter;

using Fanex.Data.Repository;
using MediatR;

public class CurrencyHandler(IDynamicRepository repository)
    : IRequestHandler<CurrencyHttpParameter, IEnumerable<Currency>>
{
    public async Task<IEnumerable<Currency>> Handle(CurrencyHttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = new CurrencyCriteria();
        return await repository.FetchAsync<Currency>(criteria);
    }
}