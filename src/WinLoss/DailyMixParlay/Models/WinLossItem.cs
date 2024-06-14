namespace DailyMixParlay.Models
{
    public class WinLossItem
    {
        public int Id { get; set; }

        public int BetCount { get; set; }

        public decimal MemberTurnover { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal SMAWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal RealIncome { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal Margin { get; set; }

        public decimal ExtraPT { get; set; }

        public decimal MemberBonus { get; set; }

        public decimal HouseBonus { get; set; }

        public string Status { get; set; }

        public string Combinations { get;set; }

        public bool LiveType { get;set; }
    }
}
