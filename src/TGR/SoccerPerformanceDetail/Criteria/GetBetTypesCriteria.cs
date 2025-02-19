namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data.Repository;
using SoccerPerformanceDetail.Model;

public class GetBetTypesCriteria : CriteriaBase
{
    public GetBetTypesCriteria() : this(BettypeViewPermission.All) { }

    public GetBetTypesCriteria(BettypeViewPermission permission)
    {
        BettypePermission = (int)permission;
    }

    public int UserId { get; set; }

    public int BettypePermission { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_UserSetting_GetBettypes";
    }
    
    public override bool IsValid()
    {
        return true;
    }
}