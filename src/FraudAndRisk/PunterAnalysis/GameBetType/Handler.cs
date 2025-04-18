namespace PunterAnalysis.GameBetType;

using AutoMapper;
using MediatR;
using Model;
using Shared;
using Utilities.LinqDynamicSum;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper),
        IRequestHandler<HttpParameterJson, SummaryEntity<GameBetTypeModel>>
{
    public async Task<SummaryEntity<GameBetTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, GameBetTypeModel>(request);
    }
}