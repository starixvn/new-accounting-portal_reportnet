namespace SBPerformance.TopWinningMember
{
    public class TopWinningMemberItemModel
    {
        public int Id { get; set; }

        public string Sport { get; set; }

        public string League { get; set; }

        public string Event { get; set; }

        public string Match { get; set; }

        public string BetType { get; set; }

        public int NoMatches { get; set; }

        public int NoBetTypes { get; set; }

        public int BetCount { get; set; }

        public decimal RealIncome { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal RealMargin { get; set; }
    }
}