using Fanex.Data.Repository;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    public class Criteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Username { get; set; }

        public override bool IsValid() => FromDate <= ToDate;

        public override string GetSettingKey() => "Acc_Rpt_AdminUpdateCredit";
    }
}