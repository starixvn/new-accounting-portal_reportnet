using Fanex.Data.Repository;
using System.Data;
using Fanex.Common;
using TraderPerformance.Criteria;
using TraderPerformance.Model;
using Utilities.DateTimes;
using Utilities.RemotingRef;

namespace TraderPerformance.Repositories;

public interface TraderPerformanceSoccerRepository
{
    Task<TraderPerformanceSoccerModel> GetTraderPerformanceSoccerReport(ReportCriteria criteria);

    Task<TraderPerformanceSoccerDetailModel> GetTraderPerformanceSoccerDetailReport(DetailReportCriteria criteria);

    Task<DataTable> GetSummaryPH<T>(T criteria);
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
            if (Converter.ToInt(dataRow["custid"]) == 0 &&
                Converter.ToDecimal(dataRow["overall_st"]) == 0)
            {
                continue;
            }

            var traderInfo = new TraderPerformanceSoccerModel
            {
                Id = Converter.ToInt(dataRow["custid"]),
                Name = Converter.ToString2(dataRow["username"]),
                MatchNumber = Converter.ToInt(dataRow["matchno"]),
                Major = Converter.ToInt(dataRow["Major"]),
                Minor1 = Converter.ToInt(dataRow["Minor1"]),
                Minor2 = Converter.ToInt(dataRow["Minor2"]),
                Minor3 = Converter.ToInt(dataRow["Minor3"]),
                Others = Converter.ToInt(dataRow["Others"])
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

        criteria.GetSettingKey();


        if (dataTable == null)
        {
            return result;
        }

        foreach (DataRow dataRow in dataTable.Rows)
        {
            var eventDate = Converter.ToDateTime(dataRow["eventdate"]);
            var traderInfo = new TraderPerformanceSoccerDetailModel
            {
                Id = Converter.ToInt(dataRow["custid"]),
                Name = Converter.ToString2(dataRow["username"]),
                EventDate = eventDate.ToMonthAndDayWithNoHyphen(),
                LeagueId = Converter.ToInt(dataRow["leagueid"]),
                HomeId = Converter.ToInt(dataRow["homeid"]),
                AwayId = Converter.ToInt(dataRow["awayid"]),
                IsNeutral = Converter.ToBool(dataRow["IsNeutral"]),
                HTHomeScore = Converter.ToString2(dataRow["hthomescore"]),
                HTAwayScore = Converter.ToString2(dataRow["htawayscore"]),
                FTHomeScore = Converter.ToString2(dataRow["fthomescore"]),
                FTAwayScore = Converter.ToString2(dataRow["ftawayscore"]),
                Maxbet = Converter.ToDecimal(dataRow["maxbet"])
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

    public async Task<DataTable> GetSummaryPH<T>(T criteria)
    {
        return (await repository.FetchDataSetAsync((ICriteria)criteria)).Tables[0];
    }

    private TraderPerformanceSoccerModel GetTraderGroupInfo(
        DataRow dataRow,
        string groupNamePrefix,
        string overallSuffix)
    {
        var traderGroupInfo = new TraderPerformanceSoccerModel
        {
            Hdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
            FirstHalfHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
            FifteenMinHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minhdp"]),
            FirstHalfOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
            OU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
            FirstHalfOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
            OneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
            FifteenMinOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15min1x2"]),
            FirstHalfNoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stNoOfCorners"]),
            NoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NoOfCorners"]),
            FifteenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minOU"]),
            FirstHalfTotalBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stTotalBookings"]),
            TotalBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_TotalBookings"]),
            NextCorner = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NextCorner"]),
            Overall = Converter.ToDecimal(dataRow[$"overall_{overallSuffix}"]),
            NextBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NextBooking"]),
            TotalBookingsFifteenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookings"]),
            TotalBookingsFifteenMinHDP = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookingsHdp"]),
            TenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10minOU"]),
            FifteenMinNoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minCorners"]),
            SingleTeamOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_SingleTeamOU"]),
            TenMinOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10min1x2"]),
            TenMinHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10minHdp"])
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
            Hdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_hdp"]),
            FirstHalfHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1sthdp"]),
            FifteenMinHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minhdp"]),
            FirstHalfOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stou"]),
            OU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_ou"]),
            FirstHalfOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1st1x2"]),
            OneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1x2"]),
            FifteenMinOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15min1x2"]),
            FirstHalfNoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stNoOfCorners"]),
            NoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NoOfCorners"]),
            FifteenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minOU"]),
            FirstHalfTotalBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_1stTotalBookings"]),
            TotalBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_TotalBookings"]),
            NextCorner = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NextCorner"]),
            Overall = Converter.ToDecimal(dataRow[$"overall_{overallSuffix}"]),
            NextBookings = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_NextBooking"]),
            TotalBookingsFifteenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookings"]),
            TotalBookingsFifteenMinHDP = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minTotalBookingsHdp"]),
            TenMinOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10minOU"]),
            FifteenMinNoOfCorners = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_15minCorners"]),
            SingleTeamOU = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_SingleTeamOU"]),
            TenMinHdp = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10minHdp"]),
            TenMinOneXTwo = Converter.ToDecimal(dataRow[$"{groupNamePrefix}_10min1x2"])
        };

        return traderGroupInfo;
    }
}