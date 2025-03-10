namespace PunterAnalysis.BetType;

using AutoMapper;
using MediatR;
using Model;
using Shared;
using Utilities.LinqDynamicSum;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<BetTypeModel>>
{
    public async Task<SummaryEntity<BetTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, BetTypeModel>(request);
    }
}