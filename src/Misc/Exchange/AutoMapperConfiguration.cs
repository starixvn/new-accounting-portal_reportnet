using AutoMapper;
using Exchange.Report;
using Utilities.LinqDynamicSum;
namespace Exchange
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeExchange(this IProfileExpression cfg)
        {
            cfg.CreateMap<HttpParameterJson, Criteria>();
            cfg.CreateMap<HttpParameterExcel, HttpParameterJson>();
            cfg.CreateMap<ExchangeEntity, ExchangeExcelEntity>();
            cfg.CreateMap<SummaryEntity<ExchangeEntity>, SummaryEntity<ExchangeExcelEntity>>();
            cfg.CreateMap<LicenseeHttpParameterJson, Criteria>();
            cfg.CreateMap<LicenseeHttpParameterExcel, LicenseeHttpParameterJson>();
            cfg.CreateMap<ExchangeItemModel, ExchangeExcelEntity>();
            cfg.CreateMap<SummaryEntity<ExchangeItemModel>, SummaryEntity<ExchangeExcelEntity>>();
            cfg.CreateMap<ExchangeModel, ExchangeExcelModel>();
            cfg.CreateMap<ExchangeEntity, ExchangeItemModel>();
            cfg.CreateMap<IEnumerable<ExchangeEntity>, List<ExchangeItemModel>>();
        }
    }
}
