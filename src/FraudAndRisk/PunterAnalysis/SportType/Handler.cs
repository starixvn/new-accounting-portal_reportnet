namespace PunterAnalysis.SportType;

using AutoMapper;
using MediatR;
using Model;
using Shared;
using Utilities.LinqDynamicSum;

public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
    : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<SportTypeModel>>
{
    public async Task<SummaryEntity<SportTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await Handle<Criteria, SportTypeModel>(request);
    }
}