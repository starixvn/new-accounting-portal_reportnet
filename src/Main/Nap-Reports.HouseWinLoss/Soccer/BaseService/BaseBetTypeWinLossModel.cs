using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Soccer.BaseService
{
    public class BaseBetTypeWinLossModel
    {
        protected BaseBetTypeWinLossModel()
        {
        }

        public SiteWinLossSummary GrandTotal { get; set; }

        public WinLoss MainTotal { get; set; }

        public WinLoss OtherTotal { get; set; }

        public WinLoss HedgeTotal { get; set; }

        public List<WinLoss> RM { get; set; } = new List<WinLoss>();
    }
}