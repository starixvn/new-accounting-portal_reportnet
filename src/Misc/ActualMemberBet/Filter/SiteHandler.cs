namespace ActualMemberBet.Filter;

using Fanex.Data.Repository;
using MediatR;

public class SiteHandler(IDynamicRepository repository) : IRequestHandler<SiteHttpParameter, IEnumerable<Site>>
{
    public async Task<IEnumerable<Site>> Handle(SiteHttpParameter request, CancellationToken cancellationToken)
    {
        var criteria = new SiteCriteria();
        return await repository.FetchAsync<Site>(criteria);
    }
}