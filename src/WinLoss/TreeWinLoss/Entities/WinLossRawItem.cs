namespace TreeWinLoss.Entities
{
    public class WinLossRawItem
    {
        public int Id { get;set; }

        public string Name { get;set; }

        public decimal Turnover { get;set; }

        public decimal WinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal WinLossTotal { get; set; }

        public decimal BaseRMTurnover { get; set; }

        public decimal BaseRMTotal { get; set; }

        public decimal BaseRMVirtualTotal { get; set; }

        public decimal BaseRMVirtualTurnover { get; set; }

        public decimal BaseRMTurnoverVarianceMinus { get; set; }

        public decimal BaseRMTurnoverVariancePlus { get; set; }

        public decimal BaseRMWinLossVarianceMinus { get; set; }

        public decimal BaseRMWinLossVariancePlus { get; set; }

        public decimal ExtraAmount { get; set; }

        public decimal BaseRMWinLossVarianceTotal { get; set; }

        public decimal BaseRMTurnoverVarianceAbsoluteTotal { get; set; }

        public string VirtualRate { get; set; }
    }
}
