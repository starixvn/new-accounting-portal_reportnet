using AutoMapper;
using MediatR;
using ResultAnalysis.Criteria;
using ResultAnalysis.Models;
using ResultAnalysis.Requests;
using ResultAnalysis.Services;

namespace ResultAnalysis.Handlers;

public class ResultAnalysisHandler(ResultAnalysisService service, IMapper mapper)
    : IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
        IRequestHandler<ResultAnalysisRequest, ResultAnalysisModel>
{
    public async Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<LeagueCriteria>(request);
        return await service.GetLeagues(criteria);
    }

    public async Task<ResultAnalysisModel> Handle(ResultAnalysisRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ResultAnalysisCriteria>(request);
        return await service.GetResultAnalysis(criteria);
    }
}