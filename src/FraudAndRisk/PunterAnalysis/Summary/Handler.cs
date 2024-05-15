using AutoMapper;
using MediatR;
using PunterAnalysis.Summary.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Summary
{
    public class Handler : IRequestHandler<HttpParameterJson, SummaryEntity<SummaryModel>>
    {
        private readonly SummaryService summaryService;

        private readonly IMapper mapper;

        public Handler(SummaryService summaryService, IMapper mapper)
        {
            this.summaryService = summaryService;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<SummaryModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<Criteria>(request);
            var summaries = await summaryService.GetSummaries(criteria);
            return summaries;
        }
    }
}