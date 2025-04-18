namespace DirectBalance;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeDirectBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<DirectBalanceItemModel, DirectBalanceItemExcelModel>();
    }
}