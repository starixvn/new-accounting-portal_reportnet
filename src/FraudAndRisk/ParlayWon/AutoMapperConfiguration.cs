using AutoMapper;
using ParlayWon.Model;

namespace ParlayWon;

public static class AutoMapperConfiguration
{
    public static void InitializeParlayWon(this IProfileExpression cfg)
    {
        cfg.CreateMap<ParlayWonTransaction, ParlayWonTransactionExcelModel>();
    }
}