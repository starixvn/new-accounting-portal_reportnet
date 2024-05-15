using AutoMapper;
using MediatR;
using PunterAnalysis.Game.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Game
{
    public class Handler : HandlerBase, IRequestHandler<HttpParameterJson, SummaryEntity<GameModel>>
    {
        public Handler(PunterAnalysisService punterAnalysisService, IMapper mapper) : base(punterAnalysisService, mapper)
        {
        }

        public async Task<SummaryEntity<GameModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, GameModel>(request);
        }
    }
}