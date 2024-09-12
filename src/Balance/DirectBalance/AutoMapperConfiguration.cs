using AutoMapper;
using DirectBalance.Model;

namespace DirectBalance;

public static class AutoMapperConfiguration
{
    public static void InitializeDirectBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<DirectBalanceItemModel, DirectBalanceItemExcelModel>();
    }
}