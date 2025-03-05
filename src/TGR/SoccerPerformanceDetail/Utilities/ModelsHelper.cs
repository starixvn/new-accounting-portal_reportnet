using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Utilities;

public static class ModelsHelper
{
    public static string MakeLeagueGroupSelectString(string leagueGroupIds, IEnumerable<LeagueGroup> leagueGroups)
    {
        var leagueGroupNames = string.Empty;

        if (string.IsNullOrEmpty(leagueGroupIds))
            return leagueGroupNames;

        if (leagueGroups == null || !leagueGroups.Any())
            return leagueGroupNames;

        var ids = leagueGroupIds.Split(',');

        foreach (var id in ids)
        {
            var group = leagueGroups
                .Where(x => x.TargetLeagueGroupId.ToString().Equals(id))
                .Select(x => x.LeagueGroupName).ToList();

            if (group != null && group.Any())
            {
                leagueGroupNames += group[0] + ", ";
            }
        }

        leagueGroupNames = leagueGroupNames.TrimEnd(new char[] { ' ', ',' });

        return leagueGroupNames;
    }

    public static string MakeBetTypeSelectString(IEnumerable<BetTypeItem> betTypes, string betTypeIds)
    {
        if (string.IsNullOrEmpty(betTypeIds) || betTypeIds == "0")
        {
            return string.Join(", ", betTypes.Select(obj => obj.BetTypeName));
        }
        else
        {
            var betTypeList = betTypeIds.Split(',').Select(int.Parse).ToList();

            var result = betTypes.Join(betTypeList, b1 => b1.BetTypeId, b2 => b2,
                (b1, b2) => new { b1.BetTypeName }).Select(x => x.BetTypeName);

            return string.Join(", ", result);
        }
    }
}