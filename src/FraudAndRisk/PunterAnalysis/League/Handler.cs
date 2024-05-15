using AutoMapper;
using MediatR;
using PunterAnalysis.League.Model;

namespace PunterAnalysis.League
{
    public class Handler : IRequestHandler<HttpParameterJson, ReportModel>
    {
        private readonly LeagueServiceFactory leagueServiceFactory;

        private readonly IMapper mapper;

        public Handler(LeagueServiceFactory leagueServiceFactory, IMapper mapper)
        {
            this.leagueServiceFactory = leagueServiceFactory;
            this.mapper = mapper;
        }

        public async Task<ReportModel> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<Criteria>(request);

            var leagueService = leagueServiceFactory.CreateLeagueService(request.PeriodType);
            return await leagueService.Query(criteria);
        }
    }
}