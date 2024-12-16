using MediatR;
using Utilities.LinqDynamicSum;

namespace Exchange.Report;

public class HttpParameterJson : IRequest<SummaryEntity<ExchangeItemModel>>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public string EventType { get; set; }

    public byte DrillLevel { get; set; }
}