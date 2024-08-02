namespace TreeWinLoss.Model
{
    public class TreeWinLossItemModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public decimal Turnover { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal WinLossTotal { get; set; }

        public decimal BaseRMTurnover { get; set; }

        public decimal BaseRMTotal { get; set; }

        public decimal BaseRMVirtualTotal { get; set; }

        public decimal BaseRMVirtualTurnover { get; set; }

        public decimal BaseRMTurnoverVariancePlus { get; set; }

        public decimal BaseRMTurnoverVarianceMinus { get; set; }

        public decimal BaseRMWinLossVariancePlus { get; set; }

        public decimal BaseRMWinLossVarianceMinus { get; set; }

        public decimal BaseRMTurnoverVarianceAbsoluteTotal { get; set; }


        public decimal BaseRMWinLossVarianceTotal { get; set; }

        public string BaseRMVarianceMargin => BaseRMTurnoverVarianceAbsoluteTotal != 0 ?
                    Math.Round((this.BaseRMWinLossVarianceTotal / BaseRMTurnoverVarianceAbsoluteTotal) * 100, 2).ToString() :
                    "N/A";

        public string VirtualBaseRMMargin => BaseRMVirtualTurnover != 0 ?
             Math.Round((this.BaseRMVirtualTotal / this.BaseRMVirtualTurnover) * 100, 2).ToString() :
             "N/A";

        public string BaseRMMargin => BaseRMTurnover != 0 ?
            Math.Round((BaseRMTotal / BaseRMTurnover) * 100, 2).ToString() :
              "N/A";

        public string VirtualRate { get; set; }

        public decimal ExtraAmount { get; set; }

        public static TreeWinLossItemModel operator +(TreeWinLossItemModel a, TreeWinLossItemModel b)
        {
            return new TreeWinLossItemModel
            {
                Name = string.Empty,
                Id = 0,
                BaseRMTurnover = a.BaseRMTurnover + b.BaseRMTurnover,
                BaseRMVirtualTurnover = a.BaseRMVirtualTurnover + b.BaseRMVirtualTurnover,
                BaseRMTurnoverVariancePlus = a.BaseRMTurnoverVariancePlus + b.BaseRMTurnoverVariancePlus,
                BaseRMTurnoverVarianceMinus = a.BaseRMTurnoverVarianceMinus + b.BaseRMTurnoverVarianceMinus,
                BaseRMTotal = a.BaseRMTotal + b.BaseRMTotal,
                BaseRMVirtualTotal = a.BaseRMVirtualTotal + b.BaseRMVirtualTotal,
                BaseRMWinLossVarianceTotal = a.BaseRMWinLossVarianceTotal + b.BaseRMWinLossVarianceTotal,
                BaseRMTurnoverVarianceAbsoluteTotal = a.BaseRMTurnoverVarianceAbsoluteTotal + b.BaseRMTurnoverVarianceAbsoluteTotal,
                BaseRMWinLossVariancePlus = a.BaseRMWinLossVariancePlus + b.BaseRMWinLossVariancePlus,
                BaseRMWinLossVarianceMinus = a.BaseRMWinLossVarianceMinus + b.BaseRMWinLossVarianceMinus,
            };
        }
    }
}
