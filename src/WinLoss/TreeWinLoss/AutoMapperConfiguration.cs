namespace TreeWinLoss;

using AutoMapper;
using Entities;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeTreeWinLoss(this IProfileExpression cfg)
    {
        cfg.CreateMap<WinLossRawItem, TreeWinLossItemModel>();
        cfg.CreateMap<TreeWinLossItemModel, TreeWinLossItemByProductExcelModel>();
        cfg.CreateMap<TreeWinLossItemModel, TreeWinLossItemByLevelExcelModel>();
    }
}