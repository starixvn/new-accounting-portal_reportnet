using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators;

public interface GridOptionColumnRemoval
{
    List<Columns> RemoveColumn(List<Columns> columns, int index);
}

public class GridOptionColumnRemovalImp : GridOptionColumnRemoval
{
    public List<Columns> RemoveColumn(List<Columns> columns, int index)
    {
        var actualIndex = GetColumnsActualIndex(columns, index);
        if (ColumnIsInRange(columns, actualIndex))
        {
            RemoveColumnByActualIndex(columns, actualIndex);
        }

        return columns;
    }

    private static int GetColumnsActualIndex(IReadOnlyList<Columns> columns, int index)
    {
        var currentIndex = 0;
        for (var i = 0; i < columns.Count; i++)
        {
            var column = columns[i];
            currentIndex += column.MergeColNumber == 0 ? 1 : column.MergeColNumber;
            if (currentIndex > index)
            {
                return i;
            }
        }

        return columns.Count;
    }

    private static bool ColumnIsInRange(IReadOnlyCollection<Columns> columns, int actualIndex)
    {
        return actualIndex < columns.Count;
    }

    private static void RemoveColumnByActualIndex(List<Columns> columns, int actualIndex)
    {
        var column = columns[actualIndex];

        var needToRemoveColumn = column.MergeColNumber <= 1;

        RemoveOrUpdateColumn(columns, column, needToRemoveColumn);

        var nextColumnIndex = needToRemoveColumn ? actualIndex : actualIndex + 1;
        UpdateColumnIndexAfterRemoveColumn(columns, nextColumnIndex);
    }

    private static void RemoveOrUpdateColumn(ICollection<Columns> columns, Columns column, bool remove)
    {
        if (remove)
        {
            columns.Remove(column);
        }
        else
        {
            column.MergeColNumber--;
        }
    }

    private static void UpdateColumnIndexAfterRemoveColumn(List<Columns> columns, int index)
    {
        for (var i = index; i < columns.Count; i++)
        {
            columns[i].ColumnIndex--;
        }
    }
}