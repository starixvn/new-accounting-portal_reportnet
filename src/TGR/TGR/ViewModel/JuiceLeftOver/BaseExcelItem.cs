using Epplus.Mapper.Annotations;

namespace TGR.ViewModel.JuiceLeftOver;

public class BaseExcelItem
{
    [Cell("A5")]
    public int No { get; set; }
}