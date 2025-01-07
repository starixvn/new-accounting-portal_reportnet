namespace HouseWinLoss.NG2.BetType;

public class ExcelLiveNonLiveQueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public bool IsNoDraw { get; set; }

    public int AliasProductID { get; set; }

    public int PermissionProfileID { get; set; } = 1;

    public int IsLive { get; set; }

    public int SportType { get; set; }

    public string Title { get; set; }

    public string LiveTitle { get; set; }

    public string NonLiveTitle { get; set; }
}