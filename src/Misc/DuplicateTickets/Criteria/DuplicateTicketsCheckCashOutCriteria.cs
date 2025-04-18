namespace DuplicateTickets.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class DuplicateTicketsCheckCashOutCriteria : CriteriaBase, INonQueryCommand
{
    [SpParam(SqlParamName = "sTransID")]
    public string TransIds { get; set; }

    [SpParam(SqlParamName = "isduplicated")]
    public bool IsDuplicated { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_DuplicatedTicketsCheckCashOut";

    public override bool IsValid() => true;
}