namespace ResultAnalysis.Models;

using System.Globalization;
using Epplus.Mapper.Annotations;

public class ResultAnalysisSummaryOfQuarterExcelModel
{
    [Cell("A6:B6")]
    public int TotalLeague { get; set; }

    [Cell("C6")]
    public int TotalMatch { get; set; }

    [Cell("D6")]
    public int FavHdpResult { get; set; }

    [Cell("E6")]
    public int UnderdogHdpResult { get; set; }

    [Cell("F6")]
    public int DrawHdpResult { get; set; }

    [Cell("G6")]
    public int OverOuResult { get; set; }

    [Cell("H6")]
    public int UnderOuResult { get; set; }

    [Cell("I6")]
    public int DrawOuResult { get; set; }

    [Cell("J6")]
    public int Fav1x2Result { get; set; }

    [Cell("K6")]
    public int Underdog1x2Result { get; set; }

    [Cell("L6")]
    public int Draw1x2Result { get; set; }
}

public class DetailedResultAnalysisOfHomeQuarterExcelModel
{
    [Cell("A9")]
    public DateTime Date { get; set; }

    [Cell("B9")]
    public string League { get; set; }

    [Cell("C9")]
    public string Match { get; set; }

    public string HDP { get; set; }

    [Cell("D9")]
    public string HdpString => HDP == "-" ? "-" : Math.Round(decimal.Parse(HDP), 2).ToString(CultureInfo.CurrentCulture);

    public string OU { get; set; }

    [Cell("E9")]
    public string OuString => OU == "-" ? "-" : Math.Round(decimal.Parse(OU), 2).ToString(CultureInfo.CurrentCulture);

    [Cell("F9")]
    public string Fav { get; set; }

    [Cell("G9")]
    public string Q1Home { get; set; }

    [Cell("H9")]
    public string Q2Home { get; set; }

    [Cell("I9")]
    public string Q3Home { get; set; }

    [Cell("J9")]
    public string Q4Home { get; set; }

    [Cell("K9")]
    public string OtHome { get; set; }

    [Cell("L9")]
    public string HtScoreHome { get; set; }

    [Cell("M9")]
    public string ShtScoreHome { get; set; }

    [Cell("N9")]
    public string FtScoreHome { get; set; }

    [Cell("O9")]
    public string ResultHDP { get; set; }

    [Cell("P9")]
    public string ResultOU { get; set; }

    [Cell("Q9")]
    public string Result1x2 { get; set; }

    [Cell("R9")]
    public string HtWM { get; set; }

    [Cell("S9")]
    public string FtWM { get; set; }
}

public class DetailedResultAnalysisOfAwayQuarterExcelModel
{
    [Cell("A9")]
    public DateTime Date { get; set; }

    [Cell("B9")]
    public string League { get; set; }

    [Cell("C9")]
    public string Match { get; set; }

    public string HDP { get; set; }

    [Cell("D9")]
    public string HdpString => HDP == "-" ? "-" : Math.Round(decimal.Parse(HDP), 2).ToString(CultureInfo.CurrentCulture);

    public string OU { get; set; }

    [Cell("E9")]
    public string OuString => OU == "-" ? "-" : Math.Round(decimal.Parse(OU), 2).ToString(CultureInfo.CurrentCulture);

    [Cell("F9")]
    public string Fav { get; set; }

    [Cell("G9")]
    public string Q1Away { get; set; }

    [Cell("H9")]
    public string Q2Away { get; set; }

    [Cell("I9")]
    public string Q3Away { get; set; }

    [Cell("J9")]
    public string Q4Away { get; set; }

    [Cell("K9")]
    public string OtAway { get; set; }

    [Cell("L9")]
    public string HtScoreAway { get; set; }

    [Cell("M9")]
    public string ShtScoreAway { get; set; }

    [Cell("N9")]
    public string FtScoreAway { get; set; }

    [Cell("O9")]
    public string ResultHDP { get; set; }

    [Cell("P9")]
    public string ResultOU { get; set; }

    [Cell("Q9")]
    public string Result1x2 { get; set; }

    [Cell("R9")]
    public string HtWM { get; set; }

    [Cell("S9")]
    public string FtWM { get; set; }
}