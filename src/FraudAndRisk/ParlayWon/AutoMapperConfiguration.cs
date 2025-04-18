namespace ParlayWon;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeParlayWon(this IProfileExpression cfg)
    {
        cfg.CreateMap<ParlayWonTransaction, ParlayWonTransactionExcelModel>();
    }
}