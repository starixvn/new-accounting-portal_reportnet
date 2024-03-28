using Fanex.Excel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
   public interface BettypeGridOptionColumnDecorator
    {
        GridOptions Build(GridOptions gridOptions, string mainColumnText, string sortColumnName, string sortDirection, int mainColumnWidth = 180);
    }
}
