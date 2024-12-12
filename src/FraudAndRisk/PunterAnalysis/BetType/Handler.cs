using AutoMapper;
using MediatR;
using PunterAnalysis.BetType.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetType
{
    public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
        : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<BetTypeModel>>
    {
        public async Task<SummaryEntity<BetTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, BetTypeModel>(request);
        }
    }
}