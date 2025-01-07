using Fanex.Data.Repository;

namespace SaveBetReport.Criteria;

public class GetLeaguesCriteria : CriteriaBase
{
    public DateTime Date { get; set; }

    public int SportType { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Special_SaveBet_GetListLeague";
}