namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;

using Common.Criteria;
using Configuration;

public class Criteria : WinLossCriteria
{
    public int IsLive { get; set; }

    public override string GetSettingKey() => "HouseWinLoss-Soccer-Minor-BetType";

    public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;
}