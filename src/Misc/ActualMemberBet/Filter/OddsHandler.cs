namespace ActualMemberBet.Filter;

using Fanex.Data.Repository;
using MediatR;

public class OddsHandler(IDynamicRepository repository) : IRequestHandler<OddsHttpParameter, IEnumerable<Odds>>
{
    public async Task<IEnumerable<Odds>> Handle(OddsHttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = new OddsCriteria();
        return await repository.FetchAsync<Odds>(criteria);
    }
}