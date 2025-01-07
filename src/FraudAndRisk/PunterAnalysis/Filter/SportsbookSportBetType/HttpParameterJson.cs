using MediatR;

namespace PunterAnalysis.Filter.SportsbookSportBetType;

public class HttpParameterJson : IRequest<IEnumerable<SportsbookSportBetTypeEntity>>;