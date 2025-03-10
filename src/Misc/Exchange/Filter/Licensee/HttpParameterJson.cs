namespace Exchange.Filter.Licensee;

using MediatR;

public class HttpParameterJson : IRequest<IEnumerable<LicenseeModel>>;