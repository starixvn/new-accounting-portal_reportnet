using Epplus.Mapper.Annotations;

namespace DirectBalance.Model;

public class DirectBalanceTotalItemExcelModel
{
    [Cell("A4:D4")]
    public string Text { get; set; }

    [Cell("E4")]
    public decimal Total { get; set; }

    [Cell("F4")]
    public decimal? Reward { get; set; }

    [Cell("G4")]
    public decimal RewardTotal { get; set; }
}