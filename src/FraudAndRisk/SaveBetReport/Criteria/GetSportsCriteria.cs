using Fanex.Data.Repository;

namespace SaveBetReport.Criteria;

public class GetSportsCriteria : CriteriaBase
{
    public DateTime Date { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Special_SaveBet_GetListSport";
}