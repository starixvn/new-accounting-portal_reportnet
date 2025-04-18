namespace UpdatedUsernameCredit;

using AutoMapper;
using Common;
using UpdatedCredit;
using UpdatedUsername;
using Criteria = UpdatedCredit.Criteria;

public static class AutoMapperConfiguration
{
    public static void InitializeUpdatedUsernameCredit(this IProfileExpression cfg)
    {
        cfg.CreateMap<BaseMessage, Criteria>();
        cfg.CreateMap<BaseMessage, UpdatedUsername.Criteria>();
        cfg.CreateMap<UpdatedUsernameEntity, UpdatedUsernameModel>();
        cfg.CreateMap<UpdatedCreditModel, UpdatedCreditExcelModel>();
        cfg.CreateMap<UpdatedCreditEntity, UpdatedCreditModel>();
        cfg.CreateMap<UpdatedUsernameModel, UpdatedUsernameExcelModel>();
    }
}