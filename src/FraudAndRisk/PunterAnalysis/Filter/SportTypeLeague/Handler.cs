using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.SportTypeLeague
{
    public class Handler : IRequestHandler<HttpParameterJson, IEnumerable<SportTypeLeagueEntity>>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

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