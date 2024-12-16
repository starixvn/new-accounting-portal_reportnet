using MediatR;
using OfficeOpenXml;

namespace Exchange.Report;

public class HttpParameterExcel : IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public string EventType { get; set; }

    public byte DrillLevel { get; set; }
}