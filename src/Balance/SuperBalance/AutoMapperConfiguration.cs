using AutoMapper;
using SuperBalance.Model;

namespace SuperBalance;

public static class AutoMapperConfiguration
{
    public static void InitializeSuperBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<BalanceItemModel, BalanceItemExcelModel>();
    }
}