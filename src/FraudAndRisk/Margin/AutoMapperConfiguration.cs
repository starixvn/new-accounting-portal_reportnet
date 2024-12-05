using AutoMapper;
using Margin.Model;

namespace Margin
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeMarginReport(this IProfileExpression cfg)
        {
            cfg.CreateMap<MarginWinLossModel, MarginWinLossExcelModel>();
        }
    }
}
