using Fanex.Data.Repository;
using System.Data;
using TraderPerformance.Criteria;
using TraderPerformance.Model;
using Utilities.DateTimes;
using Utilities.RemotingRef;

namespace TraderPerformance.Repositories
{
    public interface TraderPerformanceOtherSportsRepository
    {
        Task<TraderPerformanceOtherSportsModel> GetTraderPerformanceOtherSportsReport(ReportCriteria criteria);

        Task<TraderPerformanceOtherSportsDetailModel> GetTraderPerformanceOtherSportsDetailReport(DetailReportCriteria criteria);
    }

    public class TraderPerformanceOtherSportsRepositoryImp(
        IDynamicRepository repository,
        RemotingRefService remotingRefService) :
        TraderPerformanceOtherSportsRepository
    {
        
        public async Task<TraderPerformanceOtherSportsModel> GetTraderPerformanceOtherSportsReport(ReportCriteria criteria)
        {
            var result = new TraderPerformanceOtherSportsModel
            {
                GrandTotal = new TraderPerformanceOtherSportsModel()
            };

                var winLossGrandTotal = new TraderPerformanceOtherSportsModel();
                var turnOverGrandTotal = new TraderPerformanceOtherSportsModel();
                var liveBetCountGrandTotal = new TraderPerformanceOtherSportsModel();
                var marginGrandTotal = new TraderPerformanceOtherSportsModel();
                var commissionGrandTotal = new TraderPerformanceOtherSportsModel();

                var dataSet = await repository.FetchDataSetAsync(criteria);
                var dataTable = dataSet.Tables[0];

                if (dataTable == null)
                {
                    return result;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (Convert.ToInt32(dataRow["custid"]) == 0 &&
                        Convert.ToDecimal(dataRow["overall_st"]) == 0)
                    {
                        continue;
                    }

                    var traderInfo = new TraderPerformanceOtherSportsModel
                    {
                        Id = Convert.ToInt32(dataRow["custid"]),
                        Name = Convert.ToString(dataRow["username"]),
                        MatchNumber = Convert.ToInt32(dataRow["matchno"]),
                        Major = Convert.ToInt32(dataRow["Major"]),
                        Minor1 = Convert.ToInt32(dataRow["Minor1"]),
                        Minor2 = Convert.ToInt32(dataRow["Minor2"]),
                        Minor3 = Convert.ToInt32(dataRow["Minor3"]),
                        Others = Convert.ToInt32(dataRow["Others"])
                    };

                    var winLossInfo = GetTraderGroupInfo(dataRow, "winlost", "wl");
                    var turnOverInfo = GetTraderGroupInfo(dataRow, "stake", "st");
                    var commissionInfo = GetTraderGroupInfo(dataRow, "comm", "com");
                    var marginInfo = GetTraderGroupInfo(dataRow, "margin", "margin");
                    marginInfo.GetMarginValue(marginInfo);
                    var liveBetCountInfo = GetTraderGroupInfo(dataRow, "bet", "livebet");

                    winLossGrandTotal.CalculateTotal(winLossInfo);
                    turnOverGrandTotal.CalculateTotal(turnOverInfo);
                    commissionGrandTotal.CalculateTotal(commissionInfo);
                    liveBetCountGrandTotal.CalculateTotal(liveBetCountInfo);

                    result.GrandTotal.CalculateTotalMatchAndLeague(
                        traderInfo.MatchNumber,
                        traderInfo.Major,
                        traderInfo.Minor1,
                        traderInfo.Minor2,
                        traderInfo.Minor3,
                        traderInfo.Others);

                    winLossInfo.FormatTwoDecimalNumberData(winLossInfo);
                    turnOverInfo.FormatTwoDecimalNumberData(turnOverInfo);
                    marginInfo.FormatTwoDecimalNumberData(marginInfo);
                    commissionInfo.FormatTwoDecimalNumberData(commissionInfo);

                    traderInfo.ChildInfo.Add(winLossInfo);
                    traderInfo.ChildInfo.Add(turnOverInfo);
                    traderInfo.ChildInfo.Add(liveBetCountInfo);
                    traderInfo.ChildInfo.Add(marginInfo);
                    traderInfo.ChildInfo.Add(commissionInfo);

                    result.ReportRows.Add(traderInfo);
                }

                marginGrandTotal.CalculateMarginTotal(winLossGrandTotal, turnOverGrandTotal);

                winLossGrandTotal.FormatTwoDecimalNumberData(winLossGrandTotal);
                turnOverGrandTotal.FormatTwoDecimalNumberData(turnOverGrandTotal);
                marginGrandTotal.FormatTwoDecimalNumberData(marginGrandTotal);
                commissionGrandTotal.FormatTwoDecimalNumberData(commissionGrandTotal);

                result.GrandTotal.ChildInfo.Add(winLossGrandTotal);
                result.GrandTotal.ChildInfo.Add(turnOverGrandTotal);
                result.GrandTotal.ChildInfo.Add(liveBetCountGrandTotal);
                result.GrandTotal.ChildInfo.Add(marginGrandTotal);
                result.GrandTotal.ChildInfo.Add(commissionGrandTotal);

            return result;
        }

        
        public async Task<TraderPerformanceOtherSportsDetailModel> GetTraderPerformanceOtherSportsDetailReport(
            DetailReportCriteria criteria)
        {
            var result = new TraderPerformanceOtherSportsDetailModel
            {
                GrandTotal = new TraderPerformanceOtherSportsDetailModel()
            };
            var winLossTotal = new TraderPerformanceOtherSportsDetailModel();
            var turnOverTotal = new TraderPerformanceOtherSportsDetailModel();
            var liveBetCountTotal = new TraderPerformanceOtherSportsDetailModel();
            var marginTotal = new TraderPerformanceOtherSportsDetailModel();
            var commissionTotal = new TraderPerformanceOtherSportsDetailModel();
            criteria.GetSettingKey();


                var dataSet = await repository.FetchDataSetAsync(criteria);
                var dataTable = dataSet.Tables[0];

                if (dataTable == null)
                {
                    return result;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var eventDate = Convert.ToDateTime(dataRow["eventdate"]);
                    var traderInfo = new TraderPerformanceOtherSportsDetailModel
                    {
                        Id = Convert.ToInt32(dataRow["custid"]),
                        Name = Convert.ToString(dataRow["username"]),
                        EventDate = eventDate.ToMonthAndDayWithNoHyphen(),
                        LeagueId = Convert.ToInt32(dataRow["leagueid"]),
                        HomeId = Convert.ToInt32(dataRow["homeid"]),
                        AwayId = Convert.ToInt32(dataRow["awayid"]),
                        IsNeutral = Convert.ToBoolean(dataRow["IsNeutral"]),
                        HTHomeScore = Convert.ToString(dataRow["hthomescore"]),
                        HTAwayScore = Convert.ToString(dataRow["htawayscore"]),
                        FTHomeScore = Convert.ToString(dataRow["fthomescore"]),
                        FTAwayScore = Convert.ToString(dataRow["ftawayscore"]),
                        Maxbet = Convert.ToDecimal(dataRow["maxbet"])
                    };

                    var winLossInfo = GetTraderGroupDetailInfo(dataRow, "winlost", "wl");
                    var turnOverInfo = GetTraderGroupDetailInfo(dataRow, "stake", "st");
                    var commissionInfo = GetTraderGroupDetailInfo(dataRow, "comm", "com");
                    var marginInfo = GetTraderGroupDetailInfo(dataRow, "margin", "margin");
                    marginInfo.GetMarginValue(marginInfo);
                    var liveBetCountInfo = GetTraderGroupDetailInfo(dataRow, "bet", "livebet");

                    traderInfo.LeagueName = remotingRefService.GetLeagueName(traderInfo.LeagueId);
                    traderInfo.HomeName = remotingRefService.GetTeamName(traderInfo.HomeId);
                    traderInfo.AwayName = remotingRefService.GetTeamName(traderInfo.AwayId);

                    traderInfo.ChildInfo.Add(winLossInfo);
                    traderInfo.ChildInfo.Add(turnOverInfo);
                    traderInfo.ChildInfo.Add(liveBetCountInfo);
                    traderInfo.ChildInfo.Add(marginInfo);
                    traderInfo.ChildInfo.Add(commissionInfo);

                    winLossTotal.CalculateTotal(winLossInfo);
                    turnOverTotal.CalculateTotal(turnOverInfo);
                    liveBetCountTotal.CalculateTotal(liveBetCountInfo);
                    commissionTotal.CalculateTotal(commissionInfo);

                    result.ReportRows.Add(traderInfo);
                }

                marginTotal.CalculateMarginTotal(winLossTotal, turnOverTotal);

                winLossTotal.FormatTwoDecimalNumberData(winLossTotal);
                turnOverTotal.FormatTwoDecimalNumberData(turnOverTotal);
                marginTotal.FormatTwoDecimalNumberData(marginTotal);
                commissionTotal.FormatTwoDecimalNumberData(commissionTotal);

                result.GrandTotal.ChildInfo.Add(winLossTotal);
                result.GrandTotal.ChildInfo.Add(turnOverTotal);
                result.GrandTotal.ChildInfo.Add(liveBetCountTotal);
                result.GrandTotal.ChildInfo.Add(marginTotal);
                result.GrandTotal.ChildInfo.Add(commissionTotal);

            return result;
        }

        private TraderPerformanceOtherSportsModel GetTraderGroupInfo(
           DataRow dataRow,
           string groupNamePrefix,
           string overallSuffix)
        {
            var traderGroupInfo = new TraderPerformanceOtherSportsModel
            {
                Hdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
                FirstHalfHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
                FirstHalfOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
                OU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
                FirstHalfOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
                OneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
                FirstHalfOE = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stoe"]),
                OE = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_oe"]),
                Overall = Convert.ToDecimal(dataRow[$"overall_{overallSuffix}"])
            };

            return traderGroupInfo;
        }

        private TraderPerformanceOtherSportsDetailModel GetTraderGroupDetailInfo(
            DataRow dataRow,
            string groupNamePrefix,
            string overallSuffix)
        {
            var traderGroupInfo = new TraderPerformanceOtherSportsDetailModel
            {
                Hdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
                FirstHalfHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
                FirstHalfOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
                OU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
                FirstHalfOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
                OneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
                FirstHalfOE = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stoe"]),
                OE = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_oe"]),
                Overall = Convert.ToDecimal(dataRow[$"overall_{overallSuffix}"])
            };

            return traderGroupInfo;
        }
    }
}
