namespace Exchange;

using AutoMapper;
using Report;
using Utilities.LinqDynamicSum;

public static class AutoMapperConfiguration
{
    public static void InitializeExchange(this IProfileExpression cfg)
    {
        cfg.CreateMap<HttpParameterJson, Criteria>();
        cfg.CreateMap<LicenseeHttpParameterJson, Criteria>();
        cfg.CreateMap<HttpParameterExcel, HttpParameterJson>();
        cfg.CreateMap<ExchangeEntity, ExchangeItemModel>();

        cfg.CreateMap<ExchangeEntity, ExchangeExcelEntity>();
        cfg.CreateMap<SummaryEntity<ExchangeEntity>, SummaryEntity<ExchangeExcelEntity>>();

        cfg.CreateMap<LicenseeHttpParameterExcel, LicenseeHttpParameterJson>();
        cfg.CreateMap<ExchangeModel, ExchangeExcelModel>();
        cfg.CreateMap<ExchangeItemModel, ExchangeExcelEntity>();
        cfg.CreateMap<SummaryEntity<ExchangeItemModel>, SummaryEntity<ExchangeExcelEntity>>();
    }
}