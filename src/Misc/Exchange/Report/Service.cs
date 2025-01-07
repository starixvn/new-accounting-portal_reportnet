using AutoMapper;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace Exchange.Report;

public interface Service
{
    Task<SummaryEntity<ExchangeItemModel>> Query(HttpParameterJson message);

    Task<ExchangeModel> QueryLicensee(LicenseeHttpParameterJson message);
}

public class ServiceImp(IDynamicRepository repository, IMapper mapper) : Service
{
    public async Task<SummaryEntity<ExchangeItemModel>> Query(HttpParameterJson message)
    {
        var criteria = mapper.Map<Criteria>(message);
        var rawData = await repository.FetchAsync<ExchangeEntity>(criteria);

        var data =  mapper.Map<List<ExchangeItemModel>>(rawData);

        return data.CalculateSummaryEntity();
    }

    public async Task<ExchangeModel> QueryLicensee(LicenseeHttpParameterJson message)
    {
        var criteria = mapper.Map<Criteria>(message);
        var rawData = (await repository.FetchAsync<ExchangeEntity>(criteria)).ToList();


        var licenseeExchange = CalculateExchangeByLicensee(rawData);
        var grandTotal = CalculateGrandTotal(rawData);
        var marketTotal = CalculateTotal(rawData);

        var result = new ExchangeModel
        {
            ExchangeLicensees = licenseeExchange,
            GrandTotal = grandTotal,
            MarketTypeTotal = marketTotal
        };

        return result;
    }

    private List<SummaryEntity<ExchangeItemModel>> CalculateExchangeByLicensee(List<ExchangeEntity> rawData)
    {
        var groups = rawData.Select(x => new { x.Name }).Distinct();

        var result = from @group in groups
            let licenseeGroupRawData = rawData.Where(x => x.Name == @group.Name).ToList()
            select CalculateLicenseeGroupItems(licenseeGroupRawData);

        return result.ToList();
    }

    private SummaryEntity<ExchangeItemModel> CalculateLicenseeGroupItems(List<ExchangeEntity> rawLicensee)
    {
        var licenseeGroup = mapper.Map<List<ExchangeItemModel>>(rawLicensee);
        return licenseeGroup.CalculateSummaryEntity();
    }

    private List<ExchangeItemModel> CalculateTotal(IEnumerable<ExchangeEntity> rawData)
    {
        var groups = rawData.Select(x => new { x.MarketType }).Distinct();

        var result = from @group in groups
            let marketGroupRawData = rawData.Where(x => x.MarketType == @group.MarketType).ToList()
            select CalculateTotalByMarketItems(marketGroupRawData, @group.MarketType);

        return result.ToList();
    }

    private ExchangeItemModel CalculateTotalByMarketItems(List<ExchangeEntity> rawLicensee, string marketType)
    {

        var marketGroupRawData = mapper.Map<List<ExchangeItemModel>>(rawLicensee);
            
        var total =  marketGroupRawData.DynamicSum();
        total.MarketType = marketType;
            
        return total;
    }

    private ExchangeItemModel CalculateGrandTotal(List<ExchangeEntity> rawData)
    {
        var exchangeRawData = mapper.Map<List<ExchangeItemModel>>(rawData);
           
        var grandTotal = exchangeRawData.DynamicSum();
        grandTotal.MarketType = "Grand Total";

        return grandTotal;
    }
}