namespace LeagueWinLoss.Model;

public class MarginModel
{
    public string Date { get; set; }

    public string MonthYear => $"{Date.Substring(4, 2)}-{Date[..4]}";

    public int LeagueId { get; set; }

    public decimal CompanyMargin { get; set; }
}