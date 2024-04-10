using HouseWinLoss.Configuration;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.OutrightMatchDetail
{
    public class ExcelQueryModel : QueryModel
    {
        public string Title { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;
    }
}