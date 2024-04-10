namespace HouseWinLoss.Common.Models
{
    public class WinLossRaw
    {
        public string GroupName { get; set; }

        public int GroupId { get; set; }

        public string RowName { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal SMAWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal RealIncome { get; set; }

        public decimal VirtualIncome { get; set; }

        public decimal Variance { get; set; }

        public decimal ExtraPT { get; set; }

        public decimal RealTurnover { get; set; }

        public decimal VirtualTurnover { get; set; }

        public int IsSabaSport { get; set; }

        public int SportType { get; set; }
    }
}