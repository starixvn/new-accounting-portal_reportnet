using Fanex.Data.Repository;

namespace TraderPerformance.Criteria;

public class SummaryPHCriteria : CriteriaBase
{
    public DateTime Fromdate { get; set; }

    public DateTime Todate { get; set; }
        
    public override string GetSettingKey()
    {
        return "Main_TraderPerformance_GetReport_PH";
    }

    public override bool IsValid()
    {
        return true;
    }
}