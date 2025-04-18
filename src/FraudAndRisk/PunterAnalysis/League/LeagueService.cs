namespace PunterAnalysis.League;

using Fanex.Data.Repository;
using Model;

public interface LeagueService
{
    Task<ReportModel> Query(ICriteria criteria);
}