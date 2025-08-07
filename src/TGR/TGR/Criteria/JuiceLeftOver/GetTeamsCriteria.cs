namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetTeamsCriteria : CriteriaBase
{
    public override string GetSettingKey() => "TGRS_Team_SelList";

    public override bool IsValid() => true;
}