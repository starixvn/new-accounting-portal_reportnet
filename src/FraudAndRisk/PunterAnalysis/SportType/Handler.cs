using AutoMapper;
using MediatR;
using PunterAnalysis.Shared;
using PunterAnalysis.SportType.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.SportType
{
    public class Handler : HandlerBase, IRequestHandler<HttpParameterJson, SummaryEntity<SportTypeModel>>
    {
        public Handler(PunterAnalysisService punterAnalysisService, IMapper mapper) : base(punterAnalysisService, mapper)
        {
        }

        public async Task<SummaryEntity<SportTypeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await Handle<Criteria, SportTypeModel>(request);
        }
    }
}