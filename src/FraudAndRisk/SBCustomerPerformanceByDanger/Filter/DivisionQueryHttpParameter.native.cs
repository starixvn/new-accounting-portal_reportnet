namespace SBCustomerPerformanceByDanger.Filter;

using MediatR;

public class DivisionQueryHttpParameter : IRequest<IEnumerable<DivisionModel>>;