namespace SMAWinLoss.Summary
{
    public class WinLossEntity
    {
        public DateTime RptDate { get; set; }

        public int CustId { get; set; }

        public string Username { get; set; }

        public string Currency { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal RealIncome { get; set; }

        public decimal RealMargin => RealTurnover == 0 ? 0 : RealIncome * 100 / RealTurnover;

        public decimal LevelTurnover { get; set; }

        public decimal MemberTurnover { get; set; }

        public decimal AvgPT => MemberTurnover == 0 ? 0 : LevelTurnover * 100 / MemberTurnover;

        public string VirtualRate { get; set; }

        public int BetCount { get; set; }

        public decimal AvgBet => BetCount == 0 ? 0 : MemberTurnover / BetCount;

        public static WinLossEntity operator +(WinLossEntity a, WinLossEntity b)
        {
            return new WinLossEntity
            {
                RptDate = a.RptDate,
                CustId = 0,
                Username = string.Empty,
                Currency = string.Empty,
                RealTurnover = a.RealTurnover + b.RealTurnover,
                RealIncome = a.RealIncome + b.RealIncome,
                LevelTurnover = a.LevelTurnover + b.LevelTurnover,
                MemberTurnover = a.MemberTurnover + b.MemberTurnover,
                BetCount = a.BetCount + b.BetCount
            };
        }
    }
}