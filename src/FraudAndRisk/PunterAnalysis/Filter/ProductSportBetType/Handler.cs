namespace PunterAnalysis.Filter.ProductSportBetType;

using Fanex.Data.Repository;
using MediatR;

public class Handler(IDynamicRepository repository)
    : IRequestHandler<HttpParameterJson, IEnumerable<ProductSportBetTypeEntity>>
{
    public async Task<IEnumerable<ProductSportBetTypeEntity>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.FetchAsync<ProductSportBetTypeEntity>(criteria);
    }
}