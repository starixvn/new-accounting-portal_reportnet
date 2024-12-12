namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.OutrightMatchDetail
{
    using Configuration;

    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}