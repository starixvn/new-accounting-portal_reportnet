namespace BetConstructPerformance.Requests;

using MediatR;
using Models;

public class SportRequest : IRequest<SportModel>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public byte? LiveStatus { get; set; }

    public int MatchCount { get; set; }

    public string FilterType { get; set; }
}