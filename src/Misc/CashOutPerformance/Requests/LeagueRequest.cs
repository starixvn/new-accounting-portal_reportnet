using CashOutPerformance.Models;
using MediatR;

namespace CashOutPerformance.Requests;

public class LeagueRequest : IRequest<IEnumerable<LeagueModel>>
{
    public string SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }
}