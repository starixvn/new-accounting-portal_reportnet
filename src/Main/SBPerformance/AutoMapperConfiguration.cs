namespace SBPerformance;

using AutoMapper;
using Main;
using MarginBySite;
using MarginBySport;
using TopLosingLeague;
using TopWinningMember;

public static class AutoMapperConfiguration
{
    public static void InitializeSBPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<HttpParameter, Criteria>();
        cfg.CreateMap<MarginBySiteHttpParameter, MarginBySiteCriteria>();
        cfg.CreateMap<MarginBySportHttpParameter, MarginBySportCriteria>();
        cfg.CreateMap<TopLosingLeagueHttpParameter, TopLosingLeagueCriteria>();
        cfg.CreateMap<TopWinningMemberHttpParameter, TopWinningMemberCriteria>();
    }
}