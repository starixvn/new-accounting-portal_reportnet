namespace ActualMemberBet.Product
{
    public class ProductActualMemberBet
    {
        public string KeyId { get; set; }

        public string ProductName { get; set; }

        public int BetCount { get; set; }

        public decimal MemberTurnoverRM { get; set; }

        public decimal MemberNetWinLoss { get; set; }

        public decimal HouseRealTurnover { get; set; }

        public decimal HouseVirtualTurnover { get; set; }

        public decimal HouseRealIncome { get; set; }

        public decimal HouseVirtualWinLoss { get; set; }


        public static ProductActualMemberBet operator +(ProductActualMemberBet a, ProductActualMemberBet b)
        {
            return new ProductActualMemberBet
            {
                KeyId = string.Empty,
                ProductName = "Grand Total",
                BetCount = a.BetCount + b.BetCount,
                MemberTurnoverRM = a.MemberTurnoverRM + b.MemberTurnoverRM,
                MemberNetWinLoss = a.MemberNetWinLoss + b.MemberNetWinLoss,
                HouseRealTurnover = a.HouseRealTurnover + b.HouseRealTurnover,
                HouseVirtualTurnover = a.HouseVirtualTurnover + b.HouseVirtualTurnover,
                HouseRealIncome = a.HouseRealIncome + b.HouseRealIncome,
                HouseVirtualWinLoss = a.HouseVirtualWinLoss + b.HouseVirtualWinLoss
            };
        }
    }
}
