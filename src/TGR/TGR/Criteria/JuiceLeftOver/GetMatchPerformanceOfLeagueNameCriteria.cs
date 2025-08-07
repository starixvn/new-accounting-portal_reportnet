namespace TGR.Criteria.JuiceLeftOver;

public class GetMatchPerformanceOfLeagueNameCriteria: GetLeaguePerformanceCriteria
{
    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_LeagueAllMatch";

    public override bool IsValid() => true;
}