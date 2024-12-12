namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail
{
    using Configuration;

    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}