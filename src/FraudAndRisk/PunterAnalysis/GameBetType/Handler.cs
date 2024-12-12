using AutoMapper;
using MediatR;
using PunterAnalysis.GameBetType.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.GameBetType
{
    public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
        : HandlerBase(punterAnalysisService, mapper),
            IRequestHandler<HttpParameterJson, SummaryEntity<GameBetTypeModel>>
    {
        public async Task<SummaryEntity<GameBetTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, GameBetTypeModel>(request);
        }
    }
}