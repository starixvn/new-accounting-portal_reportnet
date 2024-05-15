using AutoMapper;
using MediatR;
using PunterAnalysis.GameBetType.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.GameBetType
{
    public class Handler : HandlerBase, IRequestHandler<HttpParameterJson, SummaryEntity<GameBetTypeModel>>
    {
        public Handler(PunterAnalysisService punterAnalysisService, IMapper mapper) : base(punterAnalysisService, mapper)
        {
        }

        public async Task<SummaryEntity<GameBetTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, GameBetTypeModel>(request);
        }
    }
}