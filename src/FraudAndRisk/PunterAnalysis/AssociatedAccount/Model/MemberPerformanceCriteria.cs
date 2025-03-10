namespace PunterAnalysis.AssociatedAccount.Model;

using Fanex.Data.Repository;

public class MemberPerformanceCriteria : CriteriaBase
{
    public string CustIds { get; set; }

    public string LeagueId { get; set; }

    public int OrderBy { get; set; }

    public int SortBy { get; set; }

    public int PeriodType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string ListCombinationId { get; set; }

    public string ListLeagueGroupId { get; set; }

    public override string GetSettingKey() => "Punter-PerformanceOfMembers";

    public override bool IsValid() => true;
}