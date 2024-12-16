namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.OutrightMatchDetail;

using Configuration;

public class ExcelQueryModel : QueryModel
{
    public string Title { get; set; }

    public new int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
}