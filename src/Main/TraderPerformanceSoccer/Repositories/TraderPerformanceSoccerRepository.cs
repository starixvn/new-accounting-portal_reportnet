using Fanex.Data.Repository;
using System.Data;
using TraderPerformanceSoccer.Criteria;
using TraderPerformanceSoccer.Model;
using Utilities.DateTimes;
using Utilities.RemotingRef;

namespace TraderPerformanceSoccer.Repositories
{
    public interface TraderPerformanceSoccerRepository
    {
        Task<TraderPerformanceSoccerModel> GetTraderPerformanceSoccerReport(ReportCriteria criteria);

        Task<TraderPerformanceSoccerDetailModel> GetTraderPerformanceSoccerDetailReport(DetailReportCriteria criteria);
    }
    public class TraderPerformanceSoccerRepositoryImp(
        IDynamicRepository repository, 
        RemotingRefService remotingRefService) 
        : TraderPerformanceSoccerRepository
    {
        public async Task<TraderPerformanceSoccerModel> GetTraderPerformanceSoccerReport(ReportCriteria criteria)
        {
            var result = new TraderPerformanceSoccerModel
            {
                GrandTotal = new TraderPerformanceSoccerModel()
            };

            var dataSet = await repository.FetchDataSetAsync(criteria);
            var dataTable = dataSet.Tables[0];

            var winLossGrandTotal = new TraderPerformanceSoccerModel();
            var turnOverGrandTotal = new TraderPerformanceSoccerModel();
            var liveBetCountGrandTotal = new TraderPerformanceSoccerModel();
            var marginGrandTotal = new TraderPerformanceSoccerModel();
            var commissionGrandTotal = new TraderPerformanceSoccerModel();

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

                TraderPerformanceSoccerModel traderInfo = new TraderPerformanceSoccerModel
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

        public async Task<TraderPerformanceSoccerDetailModel> GetTraderPerformanceSoccerDetailReport(DetailReportCriteria criteria)
        {
            var result = new TraderPerformanceSoccerDetailModel
            {
                GrandTotal = new TraderPerformanceSoccerDetailModel()
            };

            var winLossTotal = new TraderPerformanceSoccerDetailModel();
            var turnOverTotal = new TraderPerformanceSoccerDetailModel();
            var liveBetCountTotal = new TraderPerformanceSoccerDetailModel();
            var marginTotal = new TraderPerformanceSoccerDetailModel();
            var commissionTotal = new TraderPerformanceSoccerDetailModel();

            var dataSet = await repository.FetchDataSetAsync(criteria);
            var dataTable = dataSet.Tables[0];

            var settingKey = criteria.GetSettingKey();


            if (dataTable == null)
            {
                return result;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var eventDate = Convert.ToDateTime(dataRow["eventdate"]);
                TraderPerformanceSoccerDetailModel traderInfo = new TraderPerformanceSoccerDetailModel
                {
                    Id = Convert.ToInt32(dataRow["custid"]),
                    Name = Convert.ToString(dataRow["username"]),
                    EventDate = DateTimeExtensions.ToMonthAndDayWithNoHyphen(eventDate),
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

        private TraderPerformanceSoccerModel GetTraderGroupInfo(
           DataRow dataRow,
           string groupNamePrefix,
           string overallSuffix)
        {
            var traderGroupInfo = new TraderPerformanceSoccerModel
            {
                Hdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
                FirstHalfHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
                FifteenMinHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minhdp"]),
                FirstHalfOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
                OU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
                FirstHalfOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
                OneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
                FifteenMinOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15min1x2"]),
                FirstHalfNoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stNoOfCorners"]),
                NoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NoOfCorners"]),
                FifteenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minOU"]),
                FirstHalfTotalBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stTotalBookings"]),
                TotalBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_TotalBookings"]),
                NextCorner = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NextCorner"]),
                Overall = Convert.ToDecimal(dataRow[$"overall_{overallSuffix}"]),
                NextBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NextBooking"]),
                TotalBookingsFifteenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookings"]),
                TotalBookingsFifteenMinHDP = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookingsHdp"]),
                TenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10minOU"]),
                FifteenMinNoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minCorners"]),
                SingleTeamOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_SingleTeamOU"]),
                TenMinOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10min1x2"]),
                TenMinHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10minHdp"]),
            };

            return traderGroupInfo;
        }

        private TraderPerformanceSoccerDetailModel GetTraderGroupDetailInfo(
            DataRow dataRow,
            string groupNamePrefix,
            string overallSuffix)
        {
            var traderGroupInfo = new TraderPerformanceSoccerDetailModel
            {
                Hdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
                FirstHalfHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
                FifteenMinHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minhdp"]),
                FirstHalfOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
                OU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
                FirstHalfOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
                OneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
                FifteenMinOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15min1x2"]),
                FirstHalfNoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stNoOfCorners"]),
                NoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NoOfCorners"]),
                FifteenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minOU"]),
                FirstHalfTotalBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_1stTotalBookings"]),
                TotalBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_TotalBookings"]),
                NextCorner = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NextCorner"]),
                Overall = Convert.ToDecimal(dataRow[$"overall_{overallSuffix}"]),
                NextBookings = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_NextBooking"]),
                TotalBookingsFifteenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookings"]),
                TotalBookingsFifteenMinHDP = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookingsHdp"]),
                TenMinOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10minOU"]),
                FifteenMinNoOfCorners = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_15minCorners"]),
                SingleTeamOU = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_SingleTeamOU"]),
                TenMinHdp = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10minHdp"]),
                TenMinOneXTwo = Convert.ToDecimal(dataRow[$"{groupNamePrefix}_10min1x2"])
            };

            return traderGroupInfo;
        }
    }
}
