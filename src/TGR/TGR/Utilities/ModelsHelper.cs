using OfficeOpenXml;
using TGR.Constants;
using TGR.ViewModel.SoccerPerformanceDetail;

namespace TGR.Utilities;

public static class ModelsHelper
{
    public static string MakeLeagueGroupSelectString(string leagueGroupIds, IEnumerable<LeagueGroup> leagueGroups)
    {
        var leagueGroupNames = string.Empty;

        if (string.IsNullOrEmpty(leagueGroupIds))
        {
            return leagueGroupNames;
        }


        if (leagueGroups == null || !leagueGroups.Any())
        {
            return leagueGroupNames;
        }


        var ids = leagueGroupIds.Split(',');

        foreach (var id in ids)
        {
            var group = leagueGroups
                .Where(x => x.TargetLeagueGroupId.ToString().Equals(id))
                .Select(x => x.LeagueGroupName).ToList();

            if (group.Count != 0)
            {
                leagueGroupNames += group[0] + ", ";
            }
        }

        leagueGroupNames = leagueGroupNames.TrimEnd(' ', ',');

        return leagueGroupNames;
    }

    public static string MakeBetTypeSelectString(IEnumerable<BetTypeItem> betTypes, string betTypeIds)
    {
        if (string.IsNullOrEmpty(betTypeIds) || betTypeIds == "0")
        {
            return string.Join(", ", betTypes.Select(obj => obj.BetTypeName));
        }

        var betTypeList = betTypeIds.Split(',').Select(int.Parse).ToList();

        var result = betTypes.Join(betTypeList, b1 => b1.BetTypeId, b2 => b2,
            (b1, _) => new { b1.BetTypeName }).Select(x => x.BetTypeName);

        return string.Join(", ", result);
    }

    public static void FormatCellWithNumber(ExcelRange cell, decimal? data)
    {
        cell.Value = data.HasValue ? data.Value : ExcelConstants.NotAvailableText;
    }
}