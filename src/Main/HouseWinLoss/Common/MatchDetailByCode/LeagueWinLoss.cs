namespace HouseWinLoss.Common.MatchDetailByCode;

using Models;

public class LeagueWinLoss
{
    public string Name { get; set; }

    public List<WinLoss> Matches { get; set; }

    public WinLoss Total { get; set; }

    public int TotalRows { get; set; }
}