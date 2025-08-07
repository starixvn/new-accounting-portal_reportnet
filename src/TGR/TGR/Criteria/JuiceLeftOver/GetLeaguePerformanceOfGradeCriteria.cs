namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetLeaguePerformanceOfGradeCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Grade { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Grade_League";

    public override bool IsValid() => true;
}