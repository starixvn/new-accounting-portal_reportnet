using Fanex.Data;
using Fanex.Data.Repository;

namespace SuperBalance.Criteria;

public class SuperDownlineCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public int BalanceType { get; set; }

    [SpParam(Ignored = true)]
    public string CustomerType { get; set; }

    public int SuperId { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey()
    {
        var storeProcedureName = "Balance_SuperBalance_Master";

        if (!string.IsNullOrEmpty(storeProcedureName) && BalanceType == 2)
        {
            storeProcedureName += "Yesterday";
        }

        return storeProcedureName;
    }
}