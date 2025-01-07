using LeagueWinLoss.Model;
using Utilities.Sorting;

namespace LeagueWinLoss.Services;

public interface SortingService
{
    LeagueWinLossModel SortData(LeagueWinLossModel reportModel, string sortDirection, string sortColumnName);

    SiteWinLossModel SortSiteData(SiteWinLossModel reportModel, string sortDirection, string sortColumnName);
}

public class SortingServiceImp : SortingService
{
    public LeagueWinLossModel SortData(LeagueWinLossModel reportModel, string sortDirection, string sortColumnName)
    {
        SortStatisticsForPerChildGroup(reportModel, sortDirection, sortColumnName);
        return reportModel;
    }

    private static void SortStatisticsForPerChildGroup(LeagueWinLossModel reportModel, string sortDirection, string sortColumnName)
    {
        foreach (var row in reportModel.SportWinLoss)
        {
            SortExtension.SortDataByProperty(row.Leagues, sortDirection, sortColumnName, "number");
        }
    }

    public SiteWinLossModel SortSiteData(SiteWinLossModel reportModel, string sortDirection, string sortColumnName)
    {
        SortExtension.SortDataByProperty(reportModel.Sites, sortDirection, sortColumnName, "number");
        return reportModel;
    }
}