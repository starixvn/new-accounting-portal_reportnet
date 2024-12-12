using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.SportTypeLeague
{
    public class Handler(IDynamicRepository repository)
        : IRequestHandler<HttpParameterJson, IEnumerable<SportTypeLeagueEntity>>
    {
        public async Task<IEnumerable<SportTypeLeagueEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new QueryCriteria
            {
                SportId = request.SportId,
                SearchText = request.SearchText,
                SearchExactly = request.SearchExactly
            };
            return await repository.FetchAsync<SportTypeLeagueEntity>(criteria);
        }
    }
}