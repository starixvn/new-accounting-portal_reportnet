namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.Site
{
    using HouseWinLoss.Configuration;

    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}