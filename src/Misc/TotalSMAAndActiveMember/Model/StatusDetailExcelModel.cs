using Epplus.Mapper.Annotations;

namespace TotalSMAAndActiveMember.Model;

public class StatusDetailExcelModel
{
    [Cell("A5")]
    public string DateStr { get; set; }

    [Cell("B5")]
    public string GroupHeadline { get; set; }

    [Cell("C5")]
    public string Username { get; set; }

    [Cell("D5")]
    public string Status { get; set; }

    [Cell("E5")]
    public string Site { get; set; }

    [Cell("F5")]
    public string Currency { get; set; }

    [Cell("G5")]
    public string Position { get; set; }

    [Cell("H5")]
    public string CreatedTimeStr { get; set; }

    [Cell("I5")]
    public string Creator { get; set; }
}