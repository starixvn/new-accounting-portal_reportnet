namespace Margin;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeMarginReport(this IProfileExpression cfg)
    {
        cfg.CreateMap<MarginWinLossModel, MarginWinLossExcelModel>();
    }
}