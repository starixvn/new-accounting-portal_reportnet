namespace SaveBetReport.Criteria;

using Fanex.Data.Repository;

public class GetMatchesCriteria : CriteriaBase
{
    public DateTime Date { get; set; }

    public int LeagueId { get; set; }

    public int SportType { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Special_SaveBet_GetListMatch";
}