using HouseWinLoss.Common.MatchDetail;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor
{
    public interface MatchSortingService
    {
        WinLossModel SortData(WinLossModel reportModel, string sortDirection, string sortColumnName);
    }

    public class MatchSortingServiceImp : MatchSortingService
    {
        public WinLossModel SortData(WinLossModel reportModel, string sortDirection, string sortColumnName)
        {
            SortStatisticsForPerChildGroup(reportModel, sortDirection, sortColumnName);
            SortStatisticsForParentGroup(reportModel, sortDirection, sortColumnName);
            return reportModel;
        }

        private static void SortStatisticsForPerChildGroup(WinLossModel reportModel, string sortDirection, string sortColumnName)
        {
            foreach (var row in reportModel.Leagues)
            {
                Sort.SortByProperty(row.Matches, sortDirection, sortColumnName);
            }
        }

        private static void SortStatisticsForParentGroup(WinLossModel reportModel, string sortDirection, string sortColumnName)
        {
            var propInfo = typeof(WinLoss).GetProperty(sortColumnName);
            var isAsc = string.Equals(sortDirection, SortingInformation.Ascending, StringComparison.OrdinalIgnoreCase);
            reportModel.Leagues = isAsc ?
            reportModel
                .Leagues
                .OrderBy(s => propInfo.GetValue(s.Total, null))
                .ToList() :
            reportModel
                 .Leagues
                .OrderByDescending(s => propInfo.GetValue(s.Total, null))
                .ToList();
        }
    }
}