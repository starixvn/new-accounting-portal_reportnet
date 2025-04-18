namespace BetConstructPerformance.Requests;

using MediatR;
using OfficeOpenXml;

public class SportExcelRequest : IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public string SportName { get; set; }

    public byte? LiveStatus { get; set; }

    public int MatchCount { get; set; }
}