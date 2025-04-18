namespace LeagueWinLoss.Criteria;

using Fanex.Data.Repository;

public class LeagueSuggestionCriteria : CriteriaBase
{
    public string SportBettype { get; set; }

    public string SearchText { get; set; }

    public override string GetSettingKey() => "WinLoss_LeagueWinLoss_GetLeagueSuggestion";

    public override bool IsValid() => true;
}