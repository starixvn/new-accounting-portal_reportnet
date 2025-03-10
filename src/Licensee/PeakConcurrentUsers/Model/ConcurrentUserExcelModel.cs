namespace PeakConcurrentUsers.Model;

using Epplus.Mapper.Annotations;

public class ConcurrentUserExcelModel
{
    [Cell("A4")]
    public string Date { get; set; }

    [Cell("B4")]
    public decimal TotalUser { get; set; }
}