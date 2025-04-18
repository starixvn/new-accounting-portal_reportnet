namespace NegativeBalance;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeNegativeBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<NegativeBalanceUser, NegativeBalanceUserExcelModel>();
    }
}