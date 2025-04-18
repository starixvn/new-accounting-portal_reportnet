namespace DirectBalance.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class MasterDownlineCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public int BalanceType { get; set; }

    [SpParam(Ignored = true)]
    public string CustomerType { get; set; }

    public int MasterId { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey()
    {
        var storeProcedureName = "Balance_SuperBalance_Agent";

        if (BalanceType == 2)
        {
            storeProcedureName += "Yesterday";
        }

        return storeProcedureName;
    }
}