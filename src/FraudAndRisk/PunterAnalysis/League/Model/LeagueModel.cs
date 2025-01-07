namespace PunterAnalysis.League.Model;

public class LeagueModel
{
    public string LeagueName { get; set; }

    public List<PeriodGroup> PeriodGroups { get; set; }

    public static LeagueModel operator +(LeagueModel a, LeagueModel b)
    {
        var sumPeriodGroup = a.PeriodGroups.Zip(b.PeriodGroups, (x, y) => x + y).ToList();
        return new LeagueModel
        {
            LeagueName = string.Empty,
            PeriodGroups = sumPeriodGroup
        };
    }
}