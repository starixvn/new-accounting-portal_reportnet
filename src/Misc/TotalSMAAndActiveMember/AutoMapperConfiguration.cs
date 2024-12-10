using AutoMapper;
using TotalSMAAndActiveMember.Model;

namespace TotalSMAAndActiveMember;

public static class AutoMapperConfiguration
{
    public static void InitializeTotalSmaAndMemberActive(this IProfileExpression cfg)
    {
        cfg.CreateMap<StatusDetailModel, StatusDetailExcelModel>();
    }
}