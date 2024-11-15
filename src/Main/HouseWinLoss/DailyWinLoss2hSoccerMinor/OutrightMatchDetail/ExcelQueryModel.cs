namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.OutrightMatchDetail
{
    using HouseWinLoss.Configuration;

    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}