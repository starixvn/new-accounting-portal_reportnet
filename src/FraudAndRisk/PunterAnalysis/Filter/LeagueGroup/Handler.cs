namespace PunterAnalysis.Filter.LeagueGroup;

using Fanex.Data.Repository;
using MediatR;

public class Handler(IDynamicRepository repository)
    : IRequestHandler<HttpParameterJson, IEnumerable<LeagueGroupEntity>>
{
    public async Task<IEnumerable<LeagueGroupEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.FetchAsync<LeagueGroupEntity>(criteria);
    }
}