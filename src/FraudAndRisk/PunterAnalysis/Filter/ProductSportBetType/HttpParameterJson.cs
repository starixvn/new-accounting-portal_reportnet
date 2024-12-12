using MediatR;

namespace PunterAnalysis.Filter.ProductSportBetType
{
    public class HttpParameterJson : IRequest<IEnumerable<ProductSportBetTypeEntity>>;
}