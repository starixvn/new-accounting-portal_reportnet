using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.SportTypeLeague;

public class QueryCriteria : CriteriaBase
{
    public int SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }

    public override string GetSettingKey() => "Punter-SportTypeLeagueQuery";

    public override bool IsValid() => true;
}