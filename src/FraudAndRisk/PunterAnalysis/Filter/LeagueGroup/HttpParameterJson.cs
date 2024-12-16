using MediatR;

namespace PunterAnalysis.Filter.LeagueGroup;

public class HttpParameterJson : IRequest<IEnumerable<LeagueGroupEntity>>;