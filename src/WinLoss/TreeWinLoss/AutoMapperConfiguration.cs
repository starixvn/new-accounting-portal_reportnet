using AutoMapper;
using TreeWinLoss.Entities;
using TreeWinLoss.Model;

namespace TreeWinLoss;

public static class AutoMapperConfiguration
{
    public static void InitializeTreeWinLoss(this IProfileExpression cfg)
    {
        cfg.CreateMap<WinLossRawItem, TreeWinLossItemModel>();
        cfg.CreateMap<TreeWinLossItemModel, TreeWinLossItemByProductExcelModel>();
        cfg.CreateMap<TreeWinLossItemModel, TreeWinLossItemByLevelExcelModel>();
    }
}