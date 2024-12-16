using Utilities.DateTimes;

namespace TotalSMAAndActiveMember.Model;

public class StatusDetailModel
{
    public DateTime Date { get; set; }

    public string Username { get; set; }

    public string Status { get; set; }

    public string Site { get; set; }

    public string Currency { get; set; }

    public string Position { get; set; }

    public DateTime CreatedTime { get; set; }

    public string Creator { get; set; }

    #region Display fields

    public string DateStr => Date.ToShortFormatedDate();

    public string CreatedTimeStr => CreatedTime.ToShortFormatedDateWithHourMinute();

    #endregion
}