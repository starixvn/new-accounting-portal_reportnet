namespace Exchange.Report;

using MediatR;
using OfficeOpenXml;

public class LicenseeHttpParameterExcel : IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public string EventType { get; set; }
}