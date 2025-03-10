namespace PunterAnalysis.Game;

using AutoMapper;
using MediatR;
using Model;
using Shared;
using Utilities.LinqDynamicSum;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<GameModel>>
{
    public async Task<SummaryEntity<GameModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, GameModel>(request);
    }
}