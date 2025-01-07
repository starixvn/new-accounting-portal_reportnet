using AutoMapper;
using SBPerformance.Main;
using SBPerformance.MarginBySite;
using SBPerformance.MarginBySport;
using SBPerformance.TopLosingLeague;
using SBPerformance.TopWinningMember;

namespace SBPerformance;

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