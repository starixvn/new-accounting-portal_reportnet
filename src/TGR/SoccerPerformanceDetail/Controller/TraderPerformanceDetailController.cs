using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using SoccerPerformanceDetail.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerPerformanceDetail.Controller
{
    public class TraderPerformanceDetailController : ControllerBase
    {
        public JsonResult GetTraderPerformance(GetTraderPerformanceCriteria criteria)
        {
            TraderPerformanceList traderPerformance = null;

            criteria.IsExcludeLeagues = true;
            criteria.Username = criteria.Username ?? string.Empty;
            if (criteria.IsAllLeagueGroup)
            {
                criteria.LeagueGroupIds = string.Empty;
            }
            traderPerformance = this._detailReportService.GetTraderPerformance(criteria);
            JsonResult jsonResult = Json(traderPerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
