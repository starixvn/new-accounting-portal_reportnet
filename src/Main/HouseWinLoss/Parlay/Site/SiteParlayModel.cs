using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Parlay.Site
{
    public class SiteParlayModel
    {
        public List<SiteModel> Sites { get; set; }

        public WinLoss Total { get; set; }
    }
}