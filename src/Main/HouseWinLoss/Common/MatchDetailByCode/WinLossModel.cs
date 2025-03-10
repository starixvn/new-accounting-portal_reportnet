namespace HouseWinLoss.Common.MatchDetailByCode;

using Models;

public class WinLossModel
{
    public List<LeagueWinLoss> Leagues { get; set; }

    public WinLoss Total { get; set; }

    public int TotalRows { get; set; }
}