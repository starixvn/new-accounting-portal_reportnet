using AutoMapper;
using MediatR;
using PunterAnalysis.BetTypeGroup.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetTypeGroup
{
    public class Handler : HandlerBase, IRequestHandler<HttpParameterJson, SummaryEntity<BetTypeGroupModel>>
    {
        public Handler(PunterAnalysisService punterAnalysisService, IMapper mapper) : base(punterAnalysisService, mapper)
        {
        }

        public async Task<SummaryEntity<BetTypeGroupModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, BetTypeGroupModel>(request);
        }
    }
}