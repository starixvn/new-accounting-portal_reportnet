namespace DailyMixParlay.Models
{
    public class WinLossItem
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int BetCount { get; set; }

        public decimal MemberTurnover { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal SMAWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal RealIncome { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal Margin => RealTurnover == 0 ? 0 : RealIncome/RealTurnover;

        public decimal MemberBonus { get; set; }

        public decimal HouseBonus { get; set; }

        public string Status { get; set; }

        public string Combinations { get;set; }

        public bool LiveType { get;set; }

        public int BetType { get; set; }

        public int SportType { get;set; }
    }
}
