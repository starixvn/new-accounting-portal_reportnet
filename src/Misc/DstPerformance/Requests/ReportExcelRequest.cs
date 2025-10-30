using MediatR;
using OfficeOpenXml;

namespace DstPerformance.Requests;

public class ReportExcelRequest : IRequest<ExcelPackage>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public int? BetType { get; set; }

    public int? LeagueId { get; set; }

    public byte LiveStatus { get; set; }

    public string Level { get; set; }

    public int MatchCount { get; set; }
}