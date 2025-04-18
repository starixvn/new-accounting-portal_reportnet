namespace BetConstructPerformance.Requests;

using MediatR;
using Models;

public class SportRequest : IRequest<SportModel>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public byte? LiveStatus { get; set; }

    public int MatchCount { get; set; }
}