using AutoMapper;
using MediatR;
using PunterAnalysis.Shared;
using PunterAnalysis.SportType.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.SportType
{
    public class Handler(PunterAnalysisService punterAnalysisService, IMapper mapper)
        : HandlerBase(punterAnalysisService, mapper), IRequestHandler<HttpParameterJson, SummaryEntity<SportTypeModel>>
    {
        public async Task<SummaryEntity<SportTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, SportTypeModel>(request);
        }
    }
}