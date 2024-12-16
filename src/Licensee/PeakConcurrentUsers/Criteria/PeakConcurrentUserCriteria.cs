using Fanex.Data.Repository;

namespace PeakConcurrentUsers.Criteria;

public class PeakConcurrentUserCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LicenseeName { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Licensee_PeakConcurrentUser_GetReport";
}