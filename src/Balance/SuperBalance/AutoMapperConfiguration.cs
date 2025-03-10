namespace SuperBalance;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeSuperBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<BalanceItemModel, BalanceItemExcelModel>();
    }
}