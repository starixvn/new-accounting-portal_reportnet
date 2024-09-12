using Epplus.Mapper.Annotations;

namespace DirectBalance.Model;

public class DirectBalanceTotalItemExcelModel
{
    [Cell("A3:D3")]
    public string Text { get; set; }

    [Cell("E3")]
    public decimal Total { get; set; }
}