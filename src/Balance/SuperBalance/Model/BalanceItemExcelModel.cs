namespace SuperBalance.Model;

using Epplus.Mapper.Annotations;

public class BalanceItemExcelModel
{
    [Cell("A4")]
    public int No { get; set; }

    [Cell("B4")]
    public string Username { get; set; }

    [Cell("C4")]
    public string Role { get; set; }

    [Cell("D4")]
    public string CurrencyName { get; set; }

    [Cell("E4")]
    public decimal Balance { get; set; }

    [Cell("F4")]
    public decimal BaseBalance { get; set; }

    [Cell("G4")]
    public decimal Reward { get; set; }

    [Cell("H4")]
    public decimal RewardRM { get; set; }
}