using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Configuration;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    public class Criteria : WinLossCriteria
    {
        public int IsLive { get; set; }

        public override string GetSettingKey() => $"HouseWinLoss-Soccer-Minor-BetType";

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;
    }
}