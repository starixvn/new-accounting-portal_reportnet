using AutoMapper;
using MediatR;
using PunterAnalysis.BetTypeGroup.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetTypeGroup
{
    public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
        : HandlerBase(punterAnalysisService, mapper),
            IRequestHandler<HttpParameterJson, SummaryEntity<BetTypeGroupModel>>
    {
        public async Task<SummaryEntity<BetTypeGroupModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, BetTypeGroupModel>(request);
        }
    }
}