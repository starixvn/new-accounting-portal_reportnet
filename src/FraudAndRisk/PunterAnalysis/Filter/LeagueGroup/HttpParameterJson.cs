namespace PunterAnalysis.Filter.LeagueGroup;

using MediatR;

public class HttpParameterJson : IRequest<IEnumerable<LeagueGroupEntity>>;