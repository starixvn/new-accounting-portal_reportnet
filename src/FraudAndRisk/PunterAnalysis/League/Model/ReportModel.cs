namespace PunterAnalysis.League.Model
{
    public class ReportModel
    {
        public List<LeagueModel> Entities { get; set; } = new List<LeagueModel>();

        public LeagueModel Total { get; set; } = new LeagueModel();

        public List<string> GroupNames { get; set; } = new List<string>();
    }
}