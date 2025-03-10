namespace SBCustomerPerformanceByDanger.Filter;

using MediatR;

public class LeagueQueryHttpParameter : IRequest<IEnumerable<string>>
{
    public string SportBettype { get; set; }

    public string SearchText { get; set; }
}