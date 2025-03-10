namespace TotalSMAAndActiveMember;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeTotalSmaAndMemberActive(this IProfileExpression cfg)
    {
        cfg.CreateMap<StatusDetailModel, StatusDetailExcelModel>();
    }
}