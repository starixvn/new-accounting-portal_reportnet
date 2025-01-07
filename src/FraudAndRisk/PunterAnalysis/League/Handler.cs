using AutoMapper;
using MediatR;
using PunterAnalysis.League.Model;

namespace PunterAnalysis.League;

public class Handler(LeagueServiceFactory leagueServiceFactory, IMapper mapper)
    : IRequestHandler<HttpParameterJson, ReportModel>
{
    public async Task<ReportModel> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);

        var leagueService = leagueServiceFactory.CreateLeagueService(request.PeriodType);
        return await leagueService.Query(criteria);
    }
}