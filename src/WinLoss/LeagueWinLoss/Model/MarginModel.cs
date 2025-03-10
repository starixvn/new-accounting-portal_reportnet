namespace LeagueWinLoss.Model;

public class MarginModel
{
    public DateTime Date { get; set; }

    public int LeagueId { get; set; }

    public decimal CompanyMargin_NoDraw { get; set; }

    public decimal CompanyMargin { get; set; }
}