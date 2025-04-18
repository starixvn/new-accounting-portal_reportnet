namespace HouseWinLoss.DailyWinLoss2hSoccerMinor;

public static class Sort
{
    private const string Ascending = "Asc";

    public static void SortByProperty<T>(List<T> dataSource, string sortDirection, string sortColumnName)
    {
        var propInfo = typeof(T).GetProperty(sortColumnName);
        if (propInfo == null)
        {
            return;
        }

        dataSource.Sort((Comparison<T>)Compare);
        return;

        int Compare(T a, T b)
        {
            var isAsc = string.Equals(sortDirection, Ascending, StringComparison.OrdinalIgnoreCase);
            var valueA = isAsc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
            var valueB = isAsc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);
            return (valueA as IComparable)?.CompareTo(valueB) ?? 0;
        }
    }
}