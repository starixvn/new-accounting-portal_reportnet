using MediatR;
using PunterAnalysis.AssociatedAccount.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.AssociatedAccount
{
    public class Handler : IRequestHandler<HttpParameterJson, SummaryEntity<AssociatedSummaryModel>>
    {
        private readonly AssociatedFacade associatedFacade;

        public Handler(AssociatedFacade associatedFacade)
        {
            this.associatedFacade = associatedFacade;
        }

        public async Task<SummaryEntity<AssociatedSummaryModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            return await associatedFacade.Query(request);
        }
    }
}