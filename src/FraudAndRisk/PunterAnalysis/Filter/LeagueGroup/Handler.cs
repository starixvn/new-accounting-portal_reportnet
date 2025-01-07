using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.LeagueGroup;

public class Handler(IDynamicRepository repository)
    : IRequestHandler<HttpParameterJson, IEnumerable<LeagueGroupEntity>>
{
    public async Task<IEnumerable<LeagueGroupEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.FetchAsync<LeagueGroupEntity>(criteria);
    }
}