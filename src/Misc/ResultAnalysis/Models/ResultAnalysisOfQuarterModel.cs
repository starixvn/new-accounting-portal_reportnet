namespace ResultAnalysis.Models;

public class ResultAnalysisOfQuarterModel
{
    public IEnumerable<DetailedResultAnalysisOfQuarterModel> Items { get; set; }

    public BaseResultAnalysisSummaryModel Summary { get; set; }
}

public class DetailedResultAnalysisOfQuarterModel : BaseDetailedResultAnalysisModel
{
    public string Q1Home { get; set; }

    public string Q1Away { get; set; }

    public string Q2Home { get; set; }

    public string Q2Away { get; set; }

    public string Q3Home { get; set; }

    public string Q3Away { get; set; }

    public string Q4Home { get; set; }

    public string Q4Away { get; set; }

    public string OtHome { get; set; }

    public string OtAway { get; set; }

    public string ShtScoreHome { get; set; }

    public string ShtScoreAway { get; set; }

    public string HtWM { get; set; }

    public string FtWM { get; set; }
}