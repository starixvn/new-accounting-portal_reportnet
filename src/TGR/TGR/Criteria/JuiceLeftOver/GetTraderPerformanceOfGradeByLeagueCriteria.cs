namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTraderPerformanceOfGradeByLeagueCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Grade { get; set; }

    public int LeagueId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Grade_League_Trader";

    public override bool IsValid() => true;
}