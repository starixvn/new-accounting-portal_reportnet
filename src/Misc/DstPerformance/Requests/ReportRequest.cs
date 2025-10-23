using DstPerformance.Models;
using MediatR;

namespace DstPerformance.Requests;

public class ReportRequest : IRequest<ReportModel>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public int? BetType { get; set; }

    public int? LeagueId { get; set; }

    public byte LiveStatus { get; set; }
}