namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class LeagueHttpParameter : IRequest<IEnumerable<LeagueModel>>
{
    public string SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }
}