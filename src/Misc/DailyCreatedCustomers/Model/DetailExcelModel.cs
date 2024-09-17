using Epplus.Mapper.Annotations;

namespace DailyCreatedCustomers.Model;

public class DetailExcelModel
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("B5")]
    public string UserName { get; set; }

    [Cell("C5")]
    public string CreatedDate { get; set; }

    [Cell("D5")]
    public string Creator { get; set; }
}