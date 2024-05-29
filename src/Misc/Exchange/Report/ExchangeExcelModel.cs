using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public class ExchangeExcelModel
    {
        public List<SummaryEntity<ExchangeExcelEntity>> ExchangeLicensees { get; set; }

        public ExchangeExcelEntity GrandTotal { get; set; }

        public List<ExchangeExcelEntity> MarketTypeTotal { get; set; }
    }
}
