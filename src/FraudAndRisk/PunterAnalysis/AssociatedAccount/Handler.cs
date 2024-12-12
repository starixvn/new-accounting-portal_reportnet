using MediatR;
using PunterAnalysis.AssociatedAccount.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.AssociatedAccount
{
    public class Handler(AssociatedFacade associatedFacade)
        : IRequestHandler<HttpParameterJson, SummaryEntity<AssociatedSummaryModel>>
    {
        public async Task<SummaryEntity<AssociatedSummaryModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await associatedFacade.Query(request);
        }
    }
}