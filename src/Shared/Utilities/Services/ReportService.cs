using Nap_Reports.Utilities.Constants;

namespace Nap_Reports.Utilities.Services;

public static class ReportService
{
    public static string GetRoleName(int roleId)
    {
        return roleId switch
        {
            RoleConstants.Super => "Super",
            RoleConstants.Master => "Master",
            RoleConstants.Agent => "Agent",
            RoleConstants.Member => "Member",
            _ => string.Empty
        };
    }

    public static string GetBalanceType(int balanceType)
    {
        return balanceType switch
        {
            BalanceTypeConstants.Today => "Today",
            BalanceTypeConstants.Yesterday => "Yesterday",
            _ => string.Empty
        };
    }
}