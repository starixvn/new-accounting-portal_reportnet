using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public class ExchangeExcelModel
    {
        public IEnumerable<SummaryEntity<ExchangeExcelEntity>> ExchangeLicensees { get; set; }

        public ExchangeExcelEntity GrandTotal { get; set; }

        public IEnumerable<ExchangeExcelEntity> MarketTypeTotal { get; set; }
    }
}
