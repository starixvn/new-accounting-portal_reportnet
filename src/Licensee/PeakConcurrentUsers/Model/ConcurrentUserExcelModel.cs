using Epplus.Mapper.Annotations;

namespace PeakConcurrentUsers.Model;

public class ConcurrentUserExcelModel
{
    [Cell("A4")]
    public string Date { get; set; }

    [Cell("B4")]
    public decimal TotalUser { get; set; }
}