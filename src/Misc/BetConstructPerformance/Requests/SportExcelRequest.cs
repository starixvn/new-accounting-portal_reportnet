namespace BetConstructPerformance.Requests;

using MediatR;
using OfficeOpenXml;

public class SportExcelRequest : IRequest<ExcelPackage>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public string SportName { get; set; }

    public byte? LiveStatus { get; set; }

    public int MatchCount { get; set; }

    public string FilterType { get; set; }
}