namespace PunterAnalysis.Filter.SportTypeLeague;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public int SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }

    public override string GetSettingKey() => "Punter-SportTypeLeagueQuery";

    public override bool IsValid() => true;
}