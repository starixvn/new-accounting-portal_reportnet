using AutoMapper;
using MediatR;
using PunterAnalysis.Game.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Game;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<GameModel>>
{
    public async Task<SummaryEntity<GameModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, GameModel>(request);
    }
}