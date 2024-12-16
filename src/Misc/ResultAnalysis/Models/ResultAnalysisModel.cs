namespace ResultAnalysis.Models;

public class ResultAnalysisModel
{
    public IEnumerable<DetailedResultAnalysisModel> Items { get; set; }

    public ResultAnalysisSummaryModel Summary { get; set; }
}

public class ResultAnalysisSummaryModel : BaseResultAnalysisSummaryModel
{
    public int TgResult01 { get; set; }

    public int TgResult23 { get; set; }

    public int TgResult46 { get; set; }

    public int TgResult7 { get; set; }
}

public class DetailedResultAnalysisModel : BaseDetailedResultAnalysisModel
{
    public string ResultTG { get; set; }
}