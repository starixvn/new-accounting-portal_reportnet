using AutoMapper;
using BetCount.Entities;
using BetCount.Model;

namespace BetCount;

public static class AutoMapperConfiguration
{
    public static void InitializeBetCount(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetCountEntity, TransactionModel>();
        cfg.CreateMap<TransactionModel, TransactionExcelModel>();
    }
}