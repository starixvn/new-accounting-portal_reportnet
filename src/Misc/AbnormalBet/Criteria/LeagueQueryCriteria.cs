using Fanex.Data.Repository;

namespace AbnormalBet.Criteria
{
    public class LeagueQueryCriteria : CriteriaBase
    {
        public string SportId { get; set; }

        public string SearchText { get; set; }

        public bool SearchExactly { get; set; }

        public override bool IsValid() => true;

        public override string GetSettingKey() => "Acc_Rpt_AbnormalBet_LeagueSel";
    }
}