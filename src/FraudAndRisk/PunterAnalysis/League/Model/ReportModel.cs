namespace PunterAnalysis.League.Model;

public class ReportModel
{
    public List<LeagueModel> Entities { get; set; } = [];

    public LeagueModel Total { get; set; } = new();

    public List<string> GroupNames { get; set; } = [];
}