// --------------------------------------------------------------------------
// <copyright file="PerformanceDetailController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
//   <change who="Ken.Nguyen" date="2014.6.12">Add Excel export</change>
// </history>

namespace SoccerPerformanceDetail.Controller
{
    using Fanex.Logging;
    using Fanex.Logging.Extensions;
    using Fanex.Logging.Extensions.Database;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.TGRS.Core.BaseClasses;
    using Fanex.NAP.TGRS.Core.Configurations;
    using Fanex.NAP.TGRS.Core.Helpers;
    using Fanex.NAP.TGRS.Core.Resources;
    using Microsoft.AspNetCore.Mvc;
    using SoccerPerformanceDetail.Criteria;
    using SoccerPerformanceDetail.Services;
    using SoccerPerformanceDetail.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;   
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Performance detail report controller.
    /// </summary>
    public class PerformanceDetailController : ControllerBase
    {
        #region Attributes and Constructors
        
        private const string LeaguePerformanceHalfMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\LeaguePerformanceHalfMeasure.xls";
        private const string LeaguePerformanceFullMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\LeaguePerformanceFullMeasure.xls";        
        private const string TraderPerformanceHalfMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\TraderPerformanceHalfMeasure.xls";
        private const string TraderPerformanceFullMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\TraderPerformanceFullMeasure.xls";        
        private const string GradePerformanceHalfMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\GradePerformanceHalfMeasure.xls";
        private const string GradePerformanceFullMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\GradePerformanceFullMeasure.xls";        
        private const string TeamPerformanceHalfMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\TeamPerformanceHalfMeasure.xls";
        private const string TeamPerformanceFullMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\TeamPerformanceFullMeasure.xls";
        private const string BetTypePerformanceHalfMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\BetTypePerformanceHalfMeasure.xls";
        private const string BetTypePerformanceFullMeasureDetail = @"~\Areas\Reports\ExcelTemplates\DetailReports\BetTypePerformanceFullMeasure.xls";
        private const string BetChoicePerformanceOfBetTypeFullMeasure = @"~\Areas\Reports\ExcelTemplates\DetailReports\BetChoicePerformanceOfBetTypeFullMeasure.xls";
        private const string BetChoicePerformanceOfBetTypeHalfMeasure = @"~\Areas\Reports\ExcelTemplates\DetailReports\BetChoicePerformanceOfBetTypeHalfMeasure.xls";
        private const string LeaguePerformanceOfBetChoiceFullMeasure = @"~\Areas\Reports\ExcelTemplates\DetailReports\LeaguePerformanceOfBetChoiceFullMeasure.xls";
        private const string LeaguePerformanceOfBetChoiceHalfMeasure = @"~\Areas\Reports\ExcelTemplates\DetailReports\LeaguePerformanceOfBetChoiceHalfMeasure.xls";

        private readonly IDetailReportService _detailReportService;

        private readonly int[] _forecastBetType = { 1, 3, 7, 8 };

        [Simplect.Attribute.InjectObject]
        public PerformanceDetailController()
            : this(null)
        {
            DateTime minDate = new DateTime(DateTime.Today.AddYears(AppSettings.PerformanceYearRange * -1).Year, 1, 1);

            minDate = minDate < AppSettings.AvailableDate ? AppSettings.AvailableDate : minDate;

            ViewBag.MinDate = minDate;
            ViewBag.DefaultFromDate = DateTime.Today.AddDays(AppSettings.PerformanceDefaultDayRange * -1);
            ViewBag.DefaultToDate = DateTime.Today;
            ViewBag.AllowYearRange = AppSettings.PerformanceAllowYearRange;
            ViewBag.ForecastBetType = string.Join(",", this._forecastBetType);
        }

        public PerformanceDetailController(IDetailReportService detailReportService)
        {
            var service = new DetailReportService();
            service.PermissionCheck = HasRight;
            this._detailReportService = service;
        }

        #endregion Attributes and Constructors

        #region Bet Type Performance

        /// <summary>
        /// Gets BetTypePerformance page.
        /// </summary>
        /// <returns>Return BetTypePerformance View.</returns>
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public ActionResult BetTypePerformance()
        {
            var groupBetTypes = this._detailReportService.GetGroupBetTypes(CurrentSession.UserId, ReportUtil.GetBettypePermission(HasRight));

            ViewBag.GroupBetTypes = groupBetTypes;
            ViewBag.Markets = GetMarketsPermission();
            ViewBag.LeagueGroups = this._detailReportService.GetLeagueGroups();
            ViewBag.IsVirtualPerformancePermission = AppSession.IsVirtualPerformancePermission;
            ViewBag.IsRealPerformancePermission = AppSession.IsRealPerformancePermission;

            if ((!AppSession.IsSoccerLivePermission && !AppSession.IsSoccerNoneLivePermission)
                || (!AppSession.IsVirtualPerformancePermission && !AppSession.IsRealPerformancePermission))
                return View("~/Areas/Reports/Views/PerformanceDetail/NoPermission.cshtml");

            return View();
        }

        /// <summary>
        /// Gets the bet type performance.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the bet type performance data.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            if (criteria.IsAllLeagueGroup)
            {
                criteria.LeagueGroupIds = string.Empty;
            }
            BetTypePerformanceList betTypePerformance = this._detailReportService.GetBetTypePerformance(criteria);
            JsonResult jsonResult = Json(betTypePerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Gets the trader performance of a bet type.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the trader performance of a bet type data.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            TraderPerformanceList traderPerformance = this._detailReportService.GetTraderPerformanceOfBetType(criteria);
            JsonResult jsonResult = Json(traderPerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Gets the match performance of a pair of bet type and trader.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the match performance of a pair of bet type and trader data.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            MatchPerformanceList matchPerformance = this._detailReportService.GetMatchPerformanceOfBetTypeTrader(criteria);
            JsonResult jsonResult = Json(matchPerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Gets the league performance of bet type.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the league performance of bet type.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            var leaguePerformance = this._detailReportService.GetLeaguePerformanceOfBetType(criteria);
            JsonResult jsonResult = Json(leaguePerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var leaguePerformance = this._detailReportService.GetLeaguePerformanceOfBetChoice(criteria);

            JsonResult jsonResult = Json(leaguePerformance);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public ActionResult ExportLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            string templatePath = (criteria.IsReal && criteria.IsVirtual)
                    ? Server.MapPath(LeaguePerformanceOfBetChoiceFullMeasure)
                    : Server.MapPath(LeaguePerformanceOfBetChoiceHalfMeasure);

            this._detailReportService.ExportLeaguePerformanceOfBetChoice(CurrentSession.UserId, criteria, templatePath);

            return new EmptyResult();
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetBetChoicePerformanceOfBetType(GetBetChoicePerformanceOfBetTypeCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetBetTypeBetChoicePerformance(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public ActionResult ExportBetChoicePerformanceOfBetType(GetBetChoicePerformanceOfBetTypeCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            string templatePath = (criteria.IsReal && criteria.IsVirtual)
                    ? Server.MapPath(BetChoicePerformanceOfBetTypeFullMeasure)
                    : Server.MapPath(BetChoicePerformanceOfBetTypeHalfMeasure);

            this._detailReportService.ExportBetChoicePerformanceOfBetType(CurrentSession.UserId, criteria, templatePath);

            return new EmptyResult();
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetIntervalPerformanceOfBetTypeBetChoice(GetIntervalPerformanceOfBetTypeBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetIntervalPerformanceOfBetTypeBetChoice(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetPointHDPPerformanceOfBetTypeBetChoice(GetPointHDPPerformanceOfBetTypeBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetPointHDPPerformanceOfBetTypeBetChoice(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetLeaguePerformanceOfBetTypeBetChoicePointHDP(GetLeagueOfBetTypeBetChoicePointHDPCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetLeaguePerformanceOfBetTypeBetChoicePointHDP(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetTimeMachinePerformanceOfBetTypeBetChoicePointHDP(GetTimeMachineOfBetTypeBetChoicePointHDPCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetTimeMachinePerformanceOfBetTypeBetChoicePointHDP(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the trader performance of a pair of bet type and league.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the trader performance of bet type and league.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            TraderPerformanceList traderPerformance = this._detailReportService.GetTraderPerformanceOfBetTypeLeague(criteria);
            JsonResult jsonResult = Json(traderPerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetBetChoicePerformaceOfBetTypeLeague(GetBetChociePerformanceOfBetTypeLeagueCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetBetChoicePerformanceOfBetTypeLeague(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetPointHDPPerformanceOfBetTypeLeagueBetChoice(GetPointHDPPerformanceOfBetTypeLeagueBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetPointHDPPerformanceOfBetTypeLeagueBetChoice(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDP(GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDPCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDP(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetIntervalPerformanceOfBetTypeLeagueBetChoice(GetIntervalPerformanceOfBetTypeLeagueBetChoiceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;

            var result = this._detailReportService.GetIntervalPerformanceOfBetTypeLeagueBetChoice(criteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the match performance of bet type, league and trader.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains the match performance of bet type, league and trader.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public JsonResult GetMatchPerformanceOfBetTypeLeagueTrader(GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            MatchPerformanceGroup matchPerformance = this._detailReportService.GetMatchPerformanceOfBetTypeLeagueTrader(criteria);
            JsonResult jsonResult = Json(matchPerformance);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Exports the bet type performance.
        /// </summary>
        /// <param name="criteria">Criteria that are used to query data.</param>
        /// <returns>Excel file.</returns>
        [NapAuth(FunctionKey = TGRSFunctionKeys.PerformanceDetail, Policy = Constant.Policy.Read)]
        public ActionResult ExportBetTypePerformance(GetBetTypePerformanceCriteria criteria)
        {
            criteria.IsExcludeLeagues = AppSession.IsExcludingLeagues;
            string templatePath = (criteria.IsReal && criteria.IsVirtual) 
                    ? Server.MapPath(BetTypePerformanceFullMeasureDetail) 
                    : Server.MapPath(BetTypePerformanceHalfMeasureDetail);

            this._detailReportService.ExportBetTypePerformance(CurrentSession.UserId, criteria, templatePath);

            return new EmptyResult();
        }       

        #endregion Bet Type Performance

        public ActionResult GetViewScorePermission()
        {
            var result = HasRight(TGRSFunctionKeys.ScoreViewing, Constant.Policy.Read);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}