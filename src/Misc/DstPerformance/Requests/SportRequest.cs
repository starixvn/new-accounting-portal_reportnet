using DstPerformance.Models;
using MediatR;

namespace DstPerformance.Requests;

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