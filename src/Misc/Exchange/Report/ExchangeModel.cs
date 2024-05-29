using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public class ExchangeModel
    {
        public List<SummaryEntity<ExchangeItemModel>> ExchangeLicensees { get; set; }

        public ExchangeItemModel GrandTotal { get; set; }

        public List<ExchangeItemModel> MarketTypeTotal { get; set; }
    }
}
