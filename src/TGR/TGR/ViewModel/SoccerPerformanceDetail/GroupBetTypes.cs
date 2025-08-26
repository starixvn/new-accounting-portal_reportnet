namespace TGR.ViewModel.SoccerPerformanceDetail;

public class GroupBetTypes
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public int TotalDefaultCheckOfBetTypeList { get; set; }

    public IEnumerable<BetType> BetTypeList { get; set; }
}

public class BetType
{
    public int BetTypeId { get; set; }

    public string BetTypeName { get; set; }

    public bool IsDefaultCheck { get; set; }
}