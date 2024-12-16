using MediatR;

namespace Exchange.Filter.Sport;

public class HttpParameterJson : IRequest<IEnumerable<SportModel>>;