using AutoMapper;
using MediatR;
using ResultAnalysis.Criteria;
using ResultAnalysis.Models;
using ResultAnalysis.Requests;
using ResultAnalysis.Services;

namespace ResultAnalysis.Handlers
{
    public class ResultAnalysisHandler: IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
		IRequestHandler<ResultAnalysisRequest, ResultAnalysisModel>
	{
		private readonly ResultAnalysisService service;
		private readonly IMapper mapper;

		public ResultAnalysisHandler(ResultAnalysisService service, IMapper mapper)
		{
			this.service = service;
			this.mapper = mapper;
		}

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
}
