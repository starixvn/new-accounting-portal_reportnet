namespace DailyCreatedCustomers.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class DailyCreatedCustomersDetailsCriteria : CriteriaBase
{
    public DateTime Date { get; set; }

    public int RoleID { get; set; }

    public bool IsDirect { get; set; }

    [SpParam(Ignored = true)]
    public string RoleName { get; set; }

    public override string GetSettingKey()
    {
        return "Misc_DailyCreatedCustomers_GetDetailReport";
    }

    public override bool IsValid()
    {
        return true;
    }
}