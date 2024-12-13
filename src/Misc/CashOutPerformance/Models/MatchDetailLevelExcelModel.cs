using Epplus.Mapper.Annotations;

namespace CashOutPerformance.Models
{
    public class MatchDetailLevelExcelModel
    {
        private string dateTime;
        private string betTime;
        private string cashOutTime;

        [Cell("A6")]
        public string WLDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(dateTime))
                {
                    return dateTime;
                }

                var date = DateTime.Parse(dateTime);
                return date.ToString("yyyy-MM-dd");

            }
            set => dateTime = value;
        }

        [Cell("B6")]
        public string Username { get; set; }

        [Cell("C6")]
        public string SiteGroup { get; set; }

        [Cell("D6")]
        public string Currency { get; set; }

        [Cell("E6")]
        public string OrigID { get; set; }

        [Cell("F6")]
        public string BuyBackID { get; set; }

        [Cell("G6")]
        public string CashOutID { get; set; }

        [Cell("H6")]
        public string SportName { get; set; }

        [Cell("I6")]
        public string LeagueName { get; set; }

        [Cell("J6")]
        public string MatchName { get; set; }

        [Cell("K6")]
        public string BetTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(betTime))
                {
                    return betTime;
                }

                var date = DateTime.Parse(betTime);
                return date.ToString("yyyy-MM-dd hh:mm:ss tt");

            }
            set => betTime = value;
        }

        [Cell("L6")]
        public string CashOutTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(cashOutTime))
                {
                    return cashOutTime;
                }

                var date = DateTime.Parse(cashOutTime);
                return date.ToString("yyyy-MM-dd hh:mm:ss tt");

            }
            set => cashOutTime = value;
        }

        [Cell("M6")]
        public string BetTypeName { get; set; }

        [Cell("N6")]
        public string BetChoice { get; set; }

        [Cell("O6")]
        public string BetOdd { get; set; }

        [Cell("P6")]
        public string BetScore { get; set; }

        [Cell("Q6")]
        public string CurrentGoalLine { get; set; }

        [Cell("R6")]
        public string CurrentHomeOdds { get; set; }

        [Cell("S6")]
        public string CurrentAwayOdds { get; set; }

        [Cell("T6")]
        public string CurrentScore { get; set; }

        [Cell("U6")]
        public decimal COTurnover { get; set; }

        [Cell("V6")]
        public decimal BBAmount { get; set; }

        [Cell("W6")]
        public decimal MemberWL { get; set; }

        [Cell("X6")]
        public decimal COTurnover_RM { get; set; }

        [Cell("Y6")]
        public decimal? ActualTurnover { get; set; }

        [Cell("Z6")]
        public decimal BBAmount_RM { get; set; }

        [Cell("AA6")]
        public decimal MemberWL_RM { get; set; }

        [Cell("AB6")]
        public decimal? AssumedPayout { get; set; }

        [Cell("AC6")]
        public decimal? HypGGR { get; set; }

        [Cell("AD6")]
        public decimal? COProfit { get; set; }

        [Cell("AE6")]
        public decimal? Margin { get; set; }

        [Cell("AF6")]
        public decimal Comm_RM { get; set; }

        [Cell("AG6")]
        public decimal COIncome_RM { get; set; }

        [Cell("AH6")]
        public decimal TotalIncome_RM { get; set; }
    }
}