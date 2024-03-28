using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.ProductBetType
{
    public class ProductBetTypeWinLoss
    {
        public IEnumerable<WinLoss> BetTypes { get; set; }

        public WinLoss GrandTotal { get; set; }
    }
}
