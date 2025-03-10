namespace HouseWinLoss.Common.BasicWinLoss;

using Extensions;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Models;

public interface BasicGridDataRowBuilder
{
    GridDataRow BuildDataRow(WinLoss item, HAlignment mainTextAlignment);
}

public class BasicGridDataRowBuilderImp : BasicGridDataRowBuilder
{
    public GridDataRow BuildDataRow(WinLoss item, HAlignment mainTextAlignment)
    {
        var columnIndex = 0;
        var customAttributes = $"{item.Id}";
        var name = string.IsNullOrEmpty(item.Name) ? string.Empty : item.Name;
        var isSabaSport = GetSabaSportValue(item.Id, name);
        var sabaSportCustomAttributes = $"{isSabaSport}";
        var sportTypeAttribute = $"{item.SportType}";
        var columns = new List<Columns>
        {
            new() { ColumnIndex = columnIndex++, Name = "Main", Value = name, DisplayValue = item.Name, CustomAttributes = customAttributes, CellAlignment = mainTextAlignment },
            new() { ColumnIndex= columnIndex++, Name = "MemberWinLoss", Value = item.MemberWinLoss, CustomAttributes = sabaSportCustomAttributes },
            new() { ColumnIndex = columnIndex++, Name = "SMAWinLoss", Value =  item.SMAWinLoss, CustomAttributes = sportTypeAttribute },
            new(columnIndex++, "ExtraPT", item.ExtraPT),
            new(columnIndex++, "Commission", item.Commission),
            new(columnIndex++, "Variance", item.Variance),
            new(columnIndex++, "VirtualIncome", item.VirtualIncome),
            new(columnIndex++, "RealIncome", item.RealIncome),
            new(columnIndex++, "VirtualTurnover", item.VirtualTurnover),
            new(columnIndex++, "RealTurnover", item.RealTurnover),
            new(columnIndex++, "VirtualMargin", item.ExcelVirtualMargin()),
            new(columnIndex, "RealMargin", item.ExcelRealMargin())
        };

        return new GridDataRow { Columns = columns };
    }

    private static int GetSabaSportValue(int id, string name)
    {
        var isSabaSport = 0;
        if (id < 0)
        {
            isSabaSport = 1;
        }

        if(id < 0 && name.ToLower().Contains("pingoal"))
        {
            isSabaSport = 3;
        }

        return isSabaSport;
    }
}