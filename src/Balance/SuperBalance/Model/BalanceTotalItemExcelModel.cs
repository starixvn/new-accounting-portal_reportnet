namespace SuperBalance.Model;

using Epplus.Mapper.Annotations;

public class BalanceTotalItemExcelModel
{
    [Cell("A4:E4")]
    public string Text { get; set; }

    [Cell("F4")]
    public decimal Total { get; set; }

    [Cell("G4")]
    public decimal? Reward { get; set; }

    [Cell("H4")]
    public decimal RewardTotal { get; set; }
}