using MediatR;
using System.Collections.Generic;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class DivisionQueryHttpParameter : IRequest<IEnumerable<DivisionModel>>
    {
    }
}