using Fanex.Data;
using Fanex.Data.Repository;

namespace BetList.Criteria;

public class GetScoreInformationCriteria : CriteriaBase
{
    public int MatchId { get; set; }

    public int Bettype { get; set; }

    public string TraderName { get; set; }

    [SpParam(Ignored = true)]
    public DateTime ReportDate { get; set; }

    [SpParam(Ignored = true)]
    public int SportType { get; set; }

    [SpParam(Ignored = true)]
    public string SportTypeName { get; set; }

    [SpParam(Ignored = true)]
    public string BetTypeName { get; set; }

    public int LiveIndicator { get; set; }

    [SpParam(Ignored = true)]
    public string Hedge { get; set; }

    [SpParam(Ignored = true)]
    public int LeagueId { get; set; }

    [SpParam(Ignored = true)]
    public string LeagueName { get; set; }

    [SpParam(Ignored = true)]
    public string MatchCode { get; set; }

    [SpParam(Ignored = true)]
    public int HomeTeam { get; set; }

    [SpParam(Ignored = true)]
    public string HomeTeamName { get; set; }

    [SpParam(Ignored = true)]
    public int AwayTeam { get; set; }

    [SpParam(Ignored = true)]
    public string AwayTeamName { get; set; }

    [SpParam(Ignored = true)]
    public string BetId { get; set; }

    public override string GetSettingKey() => "BetList_ScoreInformation";

    public override bool IsValid() => true;
}