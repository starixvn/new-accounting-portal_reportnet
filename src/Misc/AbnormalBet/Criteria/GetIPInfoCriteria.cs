using Fanex.Data.Repository;

namespace AbnormalBet.Criteria;

public class GetIPInfoCriteria : CriteriaBase
{
    public string IP { get; set; }

    public override string GetSettingKey() => "Acc_GetIPInfo_Full";

    public override bool IsValid() => true;
}