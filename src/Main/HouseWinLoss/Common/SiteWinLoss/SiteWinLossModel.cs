using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.SiteWinLoss
{
    public class SiteWinLossModel
    {
        public List<WinLoss> Sites { get; set; }

        public WinLoss Total { get; set; }
    }
}