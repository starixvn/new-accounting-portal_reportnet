namespace PunterAnalysis.AssociatedAccount;

using MediatR;
using Model;
using Utilities.LinqDynamicSum;

public class Handler(AssociatedFacade associatedFacade)
    : IRequestHandler<HttpParameterJson, SummaryEntity<AssociatedSummaryModel>>
{
    public async Task<SummaryEntity<AssociatedSummaryModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await associatedFacade.Query(request);
    }
}