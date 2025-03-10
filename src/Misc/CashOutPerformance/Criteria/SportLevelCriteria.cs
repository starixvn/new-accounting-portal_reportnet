namespace CashOutPerformance.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class SportLevelCriteria : CriteriaBase
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string SportBettype { get; set; }

    public string Sites { get; set; }

    public string Leagues { get; set; }

    public string LeagueGroupIDs { get; set; }

    public string GlobalShowTime { get; set; }

    public byte LiveIndicator { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    [SpParam(Ignored = true)]
    public string Server { get; set; }

    public int FilterType { get; set; }

    public bool ExchangeBy { get; set; }

    public bool? EvenOddCust { get; set; }

    public byte? PriceService { get; set; }

    public override string GetSettingKey() => $"Acc_Rpt_COPerformance_SportType_{Server}";

    public override bool IsValid() => true;
}