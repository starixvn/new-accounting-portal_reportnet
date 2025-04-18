namespace PunterAnalysis.Performance.Summary.Criteria;

using Fanex.Data.Repository;

public class MonthCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Depth { get; set; }

    public string Username { get; set; }

    public override string GetSettingKey() => "Punter-Performance-Month";

    public override bool IsValid() => true;
}