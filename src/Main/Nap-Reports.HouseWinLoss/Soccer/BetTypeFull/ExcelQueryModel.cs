using System;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.Soccer.BetTypeFull
{
    public class ExcelQueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public int IsMajor { get; set; }

        public string BetTypeFullTitle { get; set; }

        public string BetTypeLiveTitle { get; set; }

        public string BetTypeNonLiveTitle { get; set; }

        public bool IsNoDraw { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

        public int PermissionProfileID { get; set; }
    }
}