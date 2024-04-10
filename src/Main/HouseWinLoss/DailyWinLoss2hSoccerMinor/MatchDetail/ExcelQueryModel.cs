using HouseWinLoss.Configuration;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail
{
    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}