using MediatR;

namespace Exchange.Filter.Licensee
{
    public class HttpParameterJson : IRequest<IEnumerable<LicenseeModel>>
    {
    }
}