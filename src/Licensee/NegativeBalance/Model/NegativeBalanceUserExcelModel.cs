namespace NegativeBalance.Model;

using Epplus.Mapper.Annotations;

public class NegativeBalanceUserExcelModel
{
    [Cell("A4")]
    public string UserName { get; set; }

    [Cell("B4")]
    public double? Balance { get; set; }

    [Cell("C4")]
    public string CurrencyName { get; set; }
}