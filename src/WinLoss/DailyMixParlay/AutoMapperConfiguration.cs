namespace DailyMixParlay;

using AutoMapper;
using Models;

public static class AutoMapperConfiguration
{
    public static void InitializeParlay(this IProfileExpression cfg)
    {
        cfg.CreateMap<WinLossItem, WinLossItem>();
    }
}