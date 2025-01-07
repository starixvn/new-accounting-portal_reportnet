using AutoMapper;
using MediatR;
using PunterAnalysis.Summary.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Summary;

public class Handler(SummaryService summaryService, IMapper mapper)
    : IRequestHandler<HttpParameterJson, SummaryEntity<SummaryModel>>
{
    public async Task<SummaryEntity<SummaryModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var summaries = await summaryService.GetSummaries(criteria);
        return summaries;
    }
}