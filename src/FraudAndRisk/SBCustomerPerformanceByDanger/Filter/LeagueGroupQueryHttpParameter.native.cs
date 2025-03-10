namespace SBCustomerPerformanceByDanger.Filter;

using MediatR;

public class LeagueGroupQueryHttpParameter : IRequest<IEnumerable<LeagueGroupModel>>
{
    public string SportBettype { get; set; }
}