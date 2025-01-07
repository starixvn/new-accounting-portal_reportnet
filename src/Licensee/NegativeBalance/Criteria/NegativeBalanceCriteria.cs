namespace NegativeBalance.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class NegativeBalanceCriteria : CriteriaBase
{
    public string Site { get; set; }

    [SpParam(Ignored = true)]
    public int PageSize { get; set; }

    [SpParam(Ignored = true)]
    public int PageIndex { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Licensee_NegativeBalance_GetReport";
}