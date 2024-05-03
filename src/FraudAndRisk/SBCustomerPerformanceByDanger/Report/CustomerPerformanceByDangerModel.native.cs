namespace SBCustomerPerformanceByDanger.Report
{
    public class CustomerPerformanceByDangerModel
    {

        public string UserName { get; set; }

        public string AgentName { get; set; }

        public string MasterName { get; set; }

        public string SuperName { get; set; }

        public string Division { get; set; }

        public string Currency { get; set; }

        public string DangerLevel { get; set; }

        public decimal GivenCredit { get; set; }

        public int BetCount { get; set; }

        public decimal MemberTO { get; set; }

        public decimal MemberWL { get; set; }

        public decimal HouseTO { get; set; }

        public decimal HouseIncome { get; set; }

        public decimal AVGStake => BetCount == 0 ? 0: MemberTO / BetCount;

        public int NumberOfAssociatedAccount { get; set; }

        public decimal Margin => MemberTO == 0 ? 0 : MemberWL / MemberTO;

        public static CustomerPerformanceByDangerModel operator +(CustomerPerformanceByDangerModel a, CustomerPerformanceByDangerModel b)
        {
            return new CustomerPerformanceByDangerModel
            {
                UserName = string.Empty,
                AgentName = string.Empty,
                MasterName = string.Empty,
                SuperName = string.Empty,
                Division = string.Empty,
                Currency = string.Empty,
                DangerLevel = string.Empty,
                GivenCredit = a.GivenCredit + b.GivenCredit,
                BetCount = a.BetCount + b.BetCount,
                MemberTO = a.MemberTO + b.MemberTO,
                MemberWL = a.MemberWL + b.MemberWL,
                HouseTO = a.HouseTO + b.HouseTO,
                HouseIncome = a.HouseIncome + b.HouseIncome,
                NumberOfAssociatedAccount = a.NumberOfAssociatedAccount + b.NumberOfAssociatedAccount
            };
        }
    }
}