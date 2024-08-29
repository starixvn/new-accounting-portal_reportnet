using Epplus.Mapper.Annotations;

namespace SuperBalance.Model;

public class BalanceTotalItemExcelModel
{
    [Cell("A3:D3")]
    public string Text { get; set; }

    [Cell("E3")]
    public decimal Total { get; set; }
}