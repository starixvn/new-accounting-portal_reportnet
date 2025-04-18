namespace Exchange.Report;

using MediatR;

public class LicenseeHttpParameterJson : IRequest<ExchangeModel>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public string EventType { get; set; }

    public byte DrillLevel { get; set; }
}