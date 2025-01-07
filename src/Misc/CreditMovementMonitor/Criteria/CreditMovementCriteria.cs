using Fanex.Data;
using Fanex.Data.Repository;

namespace CreditMovementMonitor.Criteria;

public class CreditMovementCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string SiteList { get; set; }

    public long Amount { get; set; }

    [SpParam(Ignored = true)]
    public bool IsPagingCallBack { get; set; }

    [SpParam(Ignored = true)]
    public int PageIndex { get; set; }

    [SpParam(Ignored = true)]
    public int PageSize { get; set; }

    public override bool IsValid() => FromDate <= ToDate;

    public override string GetSettingKey() => "Misc_CreditMovement_GetReport";
}