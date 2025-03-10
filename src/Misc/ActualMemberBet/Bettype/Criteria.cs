namespace ActualMemberBet.Bettype;

using Fanex.Data.Repository;

public class Criteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SiteType { get; set; }

    public int CurrencyId { get; set; }

    public string OddsType { get; set; }

    public string KeyId { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Actual-Member-Bet-BetType";
}