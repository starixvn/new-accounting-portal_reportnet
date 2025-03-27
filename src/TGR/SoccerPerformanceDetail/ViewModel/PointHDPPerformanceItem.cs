namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class PointHDPPerformanceItem
{
    public PointHDPPerformanceItem() { }

    public PointHDPPerformanceItem(PointHDPPerformanceInfo info, short betType, string betOption)
    {
        PointHDPConvert = ConvertPointHDP(info.PointHDP, betType, betOption);
        PointHDP = info.PointHDP;
        NoOfLeagues = Convert.ToInt32(info.LeagueCount);
        NoOfMatches = Convert.ToInt32(info.MatchCount);
        BetCount = Convert.ToInt32(info.BetCount);
        Turnover = info.Turnover;
        WinLoss = info.WinLoss;
        Margin = info.Margin;
        TurnoverReal = info.TurnoverReal;
        WinLossReal = info.WinLossReal;
        MarginReal = info.MarginReal;
    }

    public string PointHDPConvert { get; set; }

    public decimal PointHDP { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }     
    
    private string ConvertPointHDP(decimal pointHDP, short betType, string betOption)
    {
        string newPointHDP;

        switch (betType)
        {                
            case 28: // 3-Way Handicap --- Total Goal
            case 453: // 1H 3-Way Handicap --- Total Goal
            case 477: // Corner 3-Way Handicap --- Corner
            case 478: // 1H Corner 3-Way Handicap --- Corner
                newPointHDP = "0";
                switch (betOption)
                {
                    case "1" when pointHDP > 0:
                        newPointHDP = "-" + Math.Abs(pointHDP).ToString("0.##");
                        break;
                    case "1":
                    {
                        if (pointHDP < 0)
                            newPointHDP = "+" + Math.Abs(pointHDP).ToString("0.##");
                        break;
                    }
                    case "2" when pointHDP > 0:
                        newPointHDP = "+" + Math.Abs(pointHDP).ToString("0.##");
                        break;
                    case "2":
                    {
                        if (pointHDP < 0)
                            newPointHDP = "-" + Math.Abs(pointHDP).ToString("0.##");
                        break;
                    }
                    default:
                    {
                        if (pointHDP > 0)
                            newPointHDP = "A+" + pointHDP.ToString("0.##");
                        else if (pointHDP < 0)
                            newPointHDP = "H+" + Math.Abs(pointHDP).ToString("0.##");
                        break;
                    }
                }
                break;                                  
            case 228: // SABA OU Time Machine --- Total Goal
                newPointHDP = pointHDP.ToString("0.##");
                break;
            case 229: // SABA HDP Time Machine --- Total Goal
                newPointHDP = betOption.ToUpper() == "A" ? (-1 * pointHDP).ToString("0.##") : pointHDP.ToString("0.##");
                break;
            default:
                newPointHDP = pointHDP.ToString("0.##");
                break;
        }

        return newPointHDP;
    }
}
