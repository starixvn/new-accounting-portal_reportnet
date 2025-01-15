using MediatR;
using OfficeOpenXml;

namespace BetConstructPerformance.Requests;

public class SportExcelRequest : IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public string SportName { get; set; }

    public byte? LiveStatus { get; set; }
}