using AutoMapper;
using DailyMixParlay.Models;

namespace DailyMixParlay
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeParlay(this IProfileExpression cfg)
        {
            cfg.CreateMap<WinLossItem, WinLossItem>();
        }
    }
}
