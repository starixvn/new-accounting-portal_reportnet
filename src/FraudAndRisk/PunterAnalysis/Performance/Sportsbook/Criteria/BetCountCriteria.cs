using Fanex.Data.Repository;

namespace PunterAnalysis.Performance.Sportsbook.Criteria;

public class BetCountCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Depth { get; set; }

    public string Username { get; set; }

    public override string GetSettingKey() => "Punter-Performance-SportsbookBetCount";

    public override bool IsValid() => true;
}