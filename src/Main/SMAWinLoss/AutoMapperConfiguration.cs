using AutoMapper;
using SMAWinLoss.Summary;
using UniversalReports.SMAWinLoss.VirtualRate;

namespace SMAWinLoss
{
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
