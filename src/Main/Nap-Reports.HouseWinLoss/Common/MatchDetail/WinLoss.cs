using System;

namespace Nap_Reports.HouseWinLoss.Common.MatchDetail
{
    public class WinLoss
    {
        private const int NumberDigitOfRounding = 4;

        public int MatchId { get; set; }

        public string MatchCode { get; set; }

        public DateTime EventDate { get; set; }

        public int SportType { get; set; }

        public bool IsNeutral { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public string HomeName { get; set; }

        public string AwayName { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal SMAWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal RealIncome { get; set; }

        public decimal VirtualIncome { get; set; }

        public decimal Variance { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal VirtualTurnover { get; set; }

        public decimal RealMargin => RealTurnover != 0
            ? Math.Round(RealIncome / RealTurnover, NumberDigitOfRounding, MidpointRounding.AwayFromZero)
            : 0;

        public decimal VirtualMargin => VirtualTurnover != 0
            ? Math.Round(VirtualIncome / VirtualTurnover, NumberDigitOfRounding, MidpointRounding.AwayFromZero)
            : 0;

        public decimal ExtraPT { get; set; }
    }
}