using Fanex.Data.Repository;
using PunterAnalysis.League.Model;

namespace PunterAnalysis.League
{
    public interface LeagueService
    {
        Task<ReportModel> Query(ICriteria criteria);
    }
}