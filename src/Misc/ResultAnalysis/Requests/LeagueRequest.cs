namespace ResultAnalysis.Requests;

using MediatR;
using Models;

public class LeagueRequest: IRequest<IEnumerable<LeagueModel>>
{
    public int SportId { get; set; }

    public string SearchText { get; set; }
}