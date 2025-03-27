using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Services;
using SoccerPerformanceDetail.ViewModel;

namespace SoccerPerformanceDetail.Controller
{
    public class GradePerformanceDetailController(SoccerPerformanceDetailService service) : ControllerBase
    {
        public JsonResult GetGradePerformance(GetGradePerformanceCriteria criteria)
        {
            criteria.IsExcludeLeagues = true;
            
            if (criteria.IsAllLeagueGroup)
            {
                criteria.LeagueGroupIds = string.Empty;
            }

            GradePerformanceList gradePerformance = service.GetGradePerformance(criteria);
            JsonResult jsonResult = Json(gradePerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
