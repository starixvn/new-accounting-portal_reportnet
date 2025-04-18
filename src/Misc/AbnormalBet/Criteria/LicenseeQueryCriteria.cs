namespace AbnormalBet.Criteria;

using Fanex.Data.Repository;

public class LicenseeQueryCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Reason { get; set; }

    public string Sport { get; set; }

    public string BetType { get; set; }

    public string Licensee { get; set; }

    public bool IsTest { get; set; }

    public string LeagueId { get; set; }

    public string CurrencyId { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_Rpt_AbnormalBet_Licensee";
}