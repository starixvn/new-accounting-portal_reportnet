namespace ResultAnalysis.Models;

public class BaseResultAnalysisSummaryModel
{
    public int TotalLeague { get; set; }

    public int TotalMatch { get; set; }

    public int FavHdpResult { get; set; }

    public int UnderdogHdpResult { get; set; }

    public int DrawHdpResult { get; set; }

    public int OverOuResult { get; set; }

    public int UnderOuResult { get; set; }

    public int DrawOuResult { get; set; }

    public int Fav1x2Result { get; set; }

    public int Underdog1x2Result { get; set; }

    public int Draw1x2Result { get; set; }
}

public class BaseDetailedResultAnalysisModel
{
    public DateTime Date { get; set; }

    public string League { get; set; }

    public string Match { get; set; }

    public int MatchID { get; set; }

    public string HDP { get; set; }

    public string OU { get; set; }

    public string Fav { get; set; }

    public string HtScoreHome { get; set; }

    public string HtScoreAway { get; set; }

    public string FtScoreHome { get; set; }

    public string FtScoreAway { get; set; }

    public string ResultHDP { get; set; }

    public string ResultOU { get; set; }

    public string Result1x2 { get; set; }
}