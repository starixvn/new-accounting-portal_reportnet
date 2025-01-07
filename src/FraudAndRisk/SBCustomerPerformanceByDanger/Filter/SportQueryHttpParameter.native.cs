using MediatR;

namespace SBCustomerPerformanceByDanger.Filter;

public class SportQueryHttpParameter : IRequest<List<SportModel>>;