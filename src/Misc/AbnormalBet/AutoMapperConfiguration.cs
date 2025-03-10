namespace AbnormalBet;

using AutoMapper;
using Criteria;
using HttpParameter;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeAbnormalBet(this IProfileExpression cfg)
    {
        cfg.CreateMap<LicenseeHttpParameter, LicenseeQueryCriteria>();
        cfg.CreateMap<LicenseeHttpParameterExcel, LicenseeQueryCriteria>();
        cfg.CreateMap<MemberHttpParameter, MemberQueryCriteria>();
        cfg.CreateMap<MemberHttpParameterExcel, MemberQueryCriteria>();
        cfg.CreateMap<LicenseeItemModel, LicenseeExcelModel>();
        cfg.CreateMap<MemberItemModel, MemberExcelModel>();
    }
}