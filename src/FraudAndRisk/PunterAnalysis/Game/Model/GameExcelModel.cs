using Epplus.Mapper.Annotations;

namespace PunterAnalysis.Game.Model
{
    public class GameExcelModel
    {
        [Cell("A8")]
        public string LeagueGroupName { get; set; }

        [Cell("B8")]
        public decimal WinLoss { get; set; }

        [Cell("C8")]
        public decimal Turnover { get; set; }

        [Cell("D8")]
        public object Margin => Turnover == 0
            ? (object)"N/A" :
            (object)(WinLoss / Turnover);

        [Cell("E8")]
        public int BetCount { get; set; }
    }
}