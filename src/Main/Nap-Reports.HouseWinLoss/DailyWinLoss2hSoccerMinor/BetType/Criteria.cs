using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    public class Criteria : WinLossCriteria
    {
        public int IsLive { get; set; }

        public override string GetSettingKey() => $"HouseWinLoss-Soccer-Minor-BetType";

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;
    }
}