using Epplus.Mapper.Annotations;

namespace SuperBalance.Model;

public class BalanceItemExcelModel
{
    [Cell("A3")]
    public int No { get; set; }

    [Cell("B3")]
    public string Username { get; set; }

    [Cell("C3")]
    public string CurrencyName { get; set; }

    [Cell("D3")]
    public decimal Balance { get; set; }

    [Cell("E3")]
    public decimal BaseBalance { get; set; }
}