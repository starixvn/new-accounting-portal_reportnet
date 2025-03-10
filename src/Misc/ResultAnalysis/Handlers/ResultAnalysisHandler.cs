namespace ResultAnalysis.Handlers;

using AutoMapper;
using Criteria;
using MediatR;
using Models;
using Requests;
using Services;

public class ResultAnalysisHandler(ResultAnalysisService service, IMapper mapper)
    : IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
        IRequestHandler<ResultAnalysisRequest, ResultAnalysisModel>,
        IRequestHandler<ResultAnalysisOfQuarterRequest, ResultAnalysisOfQuarterModel>
{
    public Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<LeagueCriteria>(request);
        return service.GetLeagues(criteria);
    }

    public Task<ResultAnalysisModel> Handle(ResultAnalysisRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ResultAnalysisCriteria>(request);
        return service.GetResultAnalysis(criteria);
    }

    public Task<ResultAnalysisOfQuarterModel> Handle(ResultAnalysisOfQuarterRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ResultAnalysisCriteria>(request);
        return service.GetResultAnalysisOfQuarter(criteria);
    }
}