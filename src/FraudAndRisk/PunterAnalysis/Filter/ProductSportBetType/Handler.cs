using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.ProductSportBetType
{
    public class Handler : IRequestHandler<HttpParameterJson, IEnumerable<ProductSportBetTypeEntity>>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ProductSportBetTypeEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new QueryCriteria();
            return await repository.FetchAsync<ProductSportBetTypeEntity>(criteria);
        }
    }
}