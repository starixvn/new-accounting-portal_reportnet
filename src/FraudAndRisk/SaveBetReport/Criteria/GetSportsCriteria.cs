namespace SaveBetReport.Criteria;

using Fanex.Data.Repository;

public class GetSportsCriteria : CriteriaBase
{
    public DateTime Date { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Special_SaveBet_GetListSport";
}