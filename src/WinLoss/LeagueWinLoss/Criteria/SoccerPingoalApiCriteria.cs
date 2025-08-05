using Fanex.Data.Repository;

namespace LeagueWinLoss.Criteria;

public class SoccerPingoalApiCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_API_Soccer_Pingoal";

    public override bool IsValid() => true;
}