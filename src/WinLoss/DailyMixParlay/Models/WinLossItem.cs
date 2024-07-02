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

        public static WinLossItem operator +(WinLossItem a, WinLossItem b)
        {
            return new WinLossItem
            {
                Name = "Total",
                Id = 0,
                BetCount = a.BetCount + b.BetCount,
                MemberTurnover = a.MemberTurnover + b.MemberTurnover,
                MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
                SMAWinLoss = a.SMAWinLoss + b.SMAWinLoss,
                Commission = a.Commission + b.Commission,
                RealIncome = a.RealIncome + b.RealIncome,
                RealTurnover = a.RealTurnover + b.RealTurnover,
                MemberBonus = a.MemberBonus + b.MemberBonus,
                Status = string.Empty,
                Combinations = string.Empty,
                LiveType = a.LiveType,
                BetType = a.BetType,
                SportType = a.SportType
            };
        }
    }
}
