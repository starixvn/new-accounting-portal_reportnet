namespace Exchange.Filter.Sport;

using MediatR;

public class HttpParameterJson : IRequest<IEnumerable<SportModel>>;