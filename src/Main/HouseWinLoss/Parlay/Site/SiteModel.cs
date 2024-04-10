using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Parlay.Site
{
    public class SiteModel
    {
        public List<WinLoss> Sites { get; set; }

        public WinLoss Total { get; set; }
    }
}