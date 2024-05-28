using AutoMapper;
using Fanex.Data.Repository;
using Org.BouncyCastle.Asn1.Pkcs;
using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public interface Service
    {
        Task<SummaryEntity<ExchangeItemModel>> Query(HttpParameterJson message);

        Task<ExchangeModel> QueryLicensee(LicenseeHttpParameterJson message);
    }

    public class ServiceImp : Service
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        public ServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<SummaryEntity<ExchangeItemModel>> Query(HttpParameterJson message)
        {
            var criteria = mapper.Map<Criteria>(message);
            var rawData = await repository.FetchAsync<ExchangeEntity>(criteria);
            var data = mapper.Map<IEnumerable<ExchangeItemModel>>(rawData);

            return data.CalculateSummaryEntity();
        }

        public async Task<ExchangeModel> QueryLicensee(LicenseeHttpParameterJson message)
        {
            var criteria = mapper.Map<Criteria>(message);
            var rawData = await repository.FetchAsync<ExchangeEntity>(criteria);


            var licenseeExchange = CalculateExchangeByLicensee(rawData);
            var grandTotal = CalculateGrandTotal(rawData);
            var marketTotal = CalculateTotal(rawData);
            var totalItems = rawData.Count();

            var result = new ExchangeModel
            {
                ExchangeLicensees = licenseeExchange,
                GrandTotal = grandTotal,
                MarketTypeTotal = marketTotal
            };

            return result;
        }

        private IEnumerable<SummaryEntity<ExchangeItemModel>> CalculateExchangeByLicensee(IEnumerable<ExchangeEntity> rawData)
        {
            var groups = rawData.Select(x => new { x.Name }).Distinct();

            var result = from @group in groups
                       let licenseeGroupRawData = rawData.Where(x => x.Name == @group.Name).ToArray()
                       select CalculateLicenseeGroupItems(licenseeGroupRawData);

            return result;
        }

        private SummaryEntity<ExchangeItemModel> CalculateLicenseeGroupItems(IEnumerable<ExchangeEntity> rawLicensee)
        {
            if (rawLicensee != null && !rawLicensee.Any())
            {
                return new SummaryEntity<ExchangeItemModel>();
            }

            var licenseeGroup = mapper.Map<IEnumerable<ExchangeItemModel>>(rawLicensee);
            return licenseeGroup.CalculateSummaryEntity();
        }

        private IEnumerable<ExchangeItemModel> CalculateTotal(IEnumerable<ExchangeEntity> rawData)
        {
            var groups = rawData.Select(x => new { x.MarketType }).Distinct();

            var result = from @group in groups
                         let marketGroupRawData = rawData.Where(x => x.MarketType == @group.MarketType).ToArray()
                         select CalculateTotalByMarketItems(marketGroupRawData, @group.MarketType);

            return result;
        }

        private ExchangeItemModel CalculateTotalByMarketItems(IEnumerable<ExchangeEntity> rawLicensee, string marketType)
        {
            if (rawLicensee != null && !rawLicensee.Any())
            {
                return new ExchangeItemModel();
            }

            var marketGroupRawData = mapper.Map<IEnumerable<ExchangeItemModel>>(rawLicensee);
            
            var total =  marketGroupRawData.DynamicSum();
            total.MarketType = marketType;
            
            return total;
        }

        private ExchangeItemModel CalculateGrandTotal(IEnumerable<ExchangeEntity> rawData)
        {
            if(rawData != null && !rawData.Any())
            {
                return new ExchangeItemModel();
            }

            var exchangeRawData = mapper.Map<IEnumerable<ExchangeItemModel>>(rawData);
           
            var grandTotal = exchangeRawData.DynamicSum();
            grandTotal.MarketType = "Grand Total";

            return grandTotal;
        }
    }
}
