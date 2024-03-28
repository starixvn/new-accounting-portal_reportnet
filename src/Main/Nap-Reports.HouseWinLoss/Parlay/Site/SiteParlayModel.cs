using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Parlay.Site
{
    public class SiteParlayModel
    {
        public List<SiteModel> Sites { get; set; }

        public WinLoss Total { get; set; }
    }
}