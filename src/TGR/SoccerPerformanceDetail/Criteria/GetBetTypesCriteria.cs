namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data.Repository;
using Model;

public class GetBetTypesCriteria(BettypeViewPermission permission) : CriteriaBase
{
    public GetBetTypesCriteria() : this(BettypeViewPermission.All) { }

    public int UserId { get; set; }

    public int BettypePermission { get; set; } = (int)permission;

    public override string GetSettingKey()
    {
        return "TGRS_UserSetting_GetBettypes";
    }
    
    public override bool IsValid()
    {
        return true;
    }
}