using AutoMapper;
using NegativeBalance.Model;

namespace NegativeBalance;

public static class AutoMapperConfiguration
{
    public static void InitializeNegativeBalance(this IProfileExpression cfg)
    {
        cfg.CreateMap<NegativeBalanceUser, NegativeBalanceUserExcelModel>();
    }
}