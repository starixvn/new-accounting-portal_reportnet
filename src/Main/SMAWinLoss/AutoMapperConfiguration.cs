using AutoMapper;
using SMAWinLoss.Summary;

namespace SMAWinLoss
{
    using VirtualRate;

    public static class AutoMapperConfiguration
    {
        public static void InitializeSMAWinLoss(this IProfileExpression cfg)
        {
            cfg.CreateMap<WinLossHttpParameter, WinLossCriteria>();
            cfg.CreateMap<VirtualRateHttpParameter, VirtualRateCriteria>();
            cfg.CreateMap<WinLossHttpParameterExcel, WinLossHttpParameter>();
        }
    }
}
