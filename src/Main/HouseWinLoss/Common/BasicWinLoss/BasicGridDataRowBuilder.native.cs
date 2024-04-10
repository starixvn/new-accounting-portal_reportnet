using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.Extensions;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.BasicWinLoss
{
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
            var isSabaSport = GetSabaSportValue(item.Id, item.Name);
            var sabaSportCustomAttributes = $"{isSabaSport}";
            var sportTypeAttribute = $"{item.SportType}";
            var columns = new List<Columns>
            {
                new Columns { ColumnIndex = columnIndex++, Name = "Main", Value = string.IsNullOrEmpty(item.Name) ? string.Empty : item.Name, DisplayValue = item.Name, CustomAttributes = customAttributes, CellAlignment = mainTextAlignment },
                new Columns { ColumnIndex= columnIndex++, Name = "MemberWinLoss", Value = item.MemberWinLoss, CustomAttributes = sabaSportCustomAttributes },
                new Columns { ColumnIndex = columnIndex++, Name = "SMAWinLoss", Value =  item.SMAWinLoss, CustomAttributes = sportTypeAttribute },
                 new Columns(columnIndex++, "ExtraPT", item.ExtraPT),
                new Columns(columnIndex++, "Commission", item.Commission),
                new Columns(columnIndex++, "VarianceIncome", item.Variance),
                new Columns(columnIndex++, "VirtualIncome", item.VirtualIncome),
                new Columns(columnIndex++, "RealIncome", item.RealIncome),
                new Columns(columnIndex++, "VirtualTurnover", item.VirtualTurnover),
                new Columns(columnIndex++, "RealTurnover", item.RealTurnover),
                new Columns(columnIndex++, "VirtualMargin", item.ExcelVirtualMargin()),
                new Columns(columnIndex, "RealMargin", item.ExcelRealMargin())
            };

            return new GridDataRow { Columns = columns };
        }

        private static int GetSabaSportValue(int id, string name)
        {
            var isSabaSport = 0;
            
            if(id < 0)
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
}