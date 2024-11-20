using MediatR;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class DivisionQueryHttpParameter : IRequest<IEnumerable<DivisionModel>>
    {
    }
}