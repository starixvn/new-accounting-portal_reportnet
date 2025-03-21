using Fanex.Data.Repository;

namespace LeagueWinLoss.Criteria;

public class MarginCriteria : CriteriaBase
{
    public DateTime FromDate => DateTime.Today.AddMonths(-5);

    public DateTime ToDate => DateTime.Today;

    public string LeagueId { get; set; }

    public int? SportType { get; set; }

    public string SportBettype { get; set; }

    public int? BetType { get; set; }

    public byte? LiveStatus { get; set; }

    public string Site { get; set; }

    public bool IsNoDraw { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_LeagueWL_Margin_6_Months";

    public override bool IsValid() => true;
}