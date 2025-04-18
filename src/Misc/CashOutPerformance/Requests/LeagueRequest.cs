namespace CashOutPerformance.Requests;

using MediatR;
using Models;

public class LeagueRequest : IRequest<IEnumerable<LeagueModel>>
{
    public string SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }
}