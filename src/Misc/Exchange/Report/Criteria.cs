using Fanex.Data.Repository;

namespace Exchange.Report;

public class Criteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public string EventType { get; set; }

    public byte DrillLevel { get; set; }

    public override string GetSettingKey() => "Exchange-ReportQuery";

    public override bool IsValid() => true;
}