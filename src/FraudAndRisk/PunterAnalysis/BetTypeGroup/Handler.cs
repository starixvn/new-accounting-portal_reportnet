namespace PunterAnalysis.BetTypeGroup;

using AutoMapper;
using MediatR;
using Model;
using Shared;
using Utilities.LinqDynamicSum;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper),
        IRequestHandler<HttpParameterJson, SummaryEntity<BetTypeGroupModel>>
{
    public async Task<SummaryEntity<BetTypeGroupModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, BetTypeGroupModel>(request);
    }
}