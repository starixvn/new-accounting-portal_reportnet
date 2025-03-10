namespace BetCount;

using AutoMapper;
using Entities;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeBetCount(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetCountEntity, TransactionModel>();
        cfg.CreateMap<TransactionModel, TransactionExcelModel>();
    }
}