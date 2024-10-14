using Fanex.Data.Repository;

namespace TraderPerformance.Criteria
{
    public class DetailPHCriteria : CriteriaBase
    {
        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }

        public override string GetSettingKey()
        {
            return "Main_TraderPerformance_GetDetailReport_PH";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
