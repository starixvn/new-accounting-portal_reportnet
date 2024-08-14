using Fanex.Data.Repository;

namespace PeakConcurrentUsers.Criteria
{
    public class PeakConcurrentUserCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string LicenseeName { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Licensee_PeakConcurrentUser_GetReport";
        }
    }
}
