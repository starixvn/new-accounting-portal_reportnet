using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetTraderPerformanceMonthlyWeekCriteria : CriteriaBase
{
    public int Year { get; set; }

    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_TraderPerformanceWeeklyExcel";
    }
}