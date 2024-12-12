using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.SportsbookSportBetType
{
    public class Handler(IDynamicRepository repository)
        : IRequestHandler<HttpParameterJson, IEnumerable<SportsbookSportBetTypeEntity>>
    {
        public async Task<IEnumerable<SportsbookSportBetTypeEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new QueryCriteria();
            return await repository.FetchAsync<SportsbookSportBetTypeEntity>(criteria);
        }
    }
}