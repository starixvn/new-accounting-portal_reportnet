namespace TraderPerformance.Model;

public class TraderPerformanceOtherSportsDetailModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string EventDate { get; set; }

    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public int HomeId { get; set; }

    public string HomeName { get; set; }

    public int AwayId { get; set; }

    public string AwayName { get; set; }

    public bool IsNeutral { get; set; }

    public string HTHomeScore { get; set; }

    public string HTAwayScore { get; set; }

    public string FTHomeScore { get; set; }

    public string FTAwayScore { get; set; }

    public decimal FirstHalfOU { get; set; }

    public decimal OU { get; set; }

    public decimal FirstHalfOE { get; set; }

    public decimal OE { get; set; }

    public decimal FirstHalfHdp { get; set; }

    public decimal Hdp { get; set; }

    public decimal FirstHalfOneXTwo { get; set; }

    public decimal OneXTwo { get; set; }

    public decimal Overall { get; set; }

    public decimal Maxbet { get; set; }

    public List<TraderPerformanceOtherSportsDetailModel> ChildInfo { get; set; } = [];

    public List<TraderPerformanceOtherSportsDetailModel> ReportRows { get; set; } = [];

    public TraderPerformanceOtherSportsDetailModel GrandTotal { get; set; }

    public int ReportLength { get; set; }

    public void CalculateTotal(TraderPerformanceOtherSportsDetailModel traderInfo)
    {
        FirstHalfHdp += traderInfo.FirstHalfHdp;
        Hdp += traderInfo.Hdp;
        FirstHalfOU += traderInfo.FirstHalfOU;
        OU += traderInfo.OU;
        FirstHalfOneXTwo += traderInfo.FirstHalfOneXTwo;
        OneXTwo += traderInfo.OneXTwo;
        FirstHalfOE += traderInfo.FirstHalfOE;
        OE += traderInfo.OE;
        Overall += traderInfo.Overall;
    }

        
    public void CalculateMarginTotal(TraderPerformanceOtherSportsDetailModel winLossInfo, TraderPerformanceOtherSportsDetailModel turnOverInfo)
    {
        FirstHalfHdp = turnOverInfo.FirstHalfHdp == 0 ?
            0 :
            winLossInfo.FirstHalfHdp / turnOverInfo.FirstHalfHdp * 100;
        Hdp = turnOverInfo.Hdp == 0 ?
            0 :
            winLossInfo.Hdp / turnOverInfo.Hdp * 100;
        FirstHalfOU = turnOverInfo.FirstHalfOU == 0 ?
            0 :
            winLossInfo.FirstHalfOU / turnOverInfo.FirstHalfOU * 100;
        OU = turnOverInfo.OU == 0 ?
            0 :
            winLossInfo.OU / turnOverInfo.OU * 100;
        FirstHalfOneXTwo = turnOverInfo.FirstHalfOneXTwo == 0 ?
            0 :
            winLossInfo.FirstHalfOneXTwo / turnOverInfo.FirstHalfOneXTwo * 100;
        OneXTwo = turnOverInfo.OneXTwo == 0 ?
            0 :
            winLossInfo.OneXTwo / turnOverInfo.OneXTwo * 100;
        FirstHalfOE = turnOverInfo.FirstHalfOE == 0 ?
            0 :
            winLossInfo.FirstHalfOE / turnOverInfo.FirstHalfOE * 100;
        OE = turnOverInfo.OE == 0 ?
            0 :
            winLossInfo.OE / turnOverInfo.OE * 100;
        Overall = turnOverInfo.Overall == 0 ?
            0 :
            winLossInfo.Overall / turnOverInfo.Overall * 100;
    }

    public void FormatTwoDecimalNumberData(TraderPerformanceOtherSportsDetailModel traderInfo)
    {
        FirstHalfHdp = Math.Round(traderInfo.FirstHalfHdp, 2, MidpointRounding.AwayFromZero);
        Hdp = Math.Round(traderInfo.Hdp, 2, MidpointRounding.AwayFromZero);
        FirstHalfOU = Math.Round(traderInfo.FirstHalfOU, 2, MidpointRounding.AwayFromZero);
        OU = Math.Round(traderInfo.OU, 2, MidpointRounding.AwayFromZero);
        FirstHalfOneXTwo = Math.Round(traderInfo.FirstHalfOneXTwo, 2, MidpointRounding.AwayFromZero);
        OneXTwo = Math.Round(traderInfo.OneXTwo, 2, MidpointRounding.AwayFromZero);
        FirstHalfOE = Math.Round(traderInfo.FirstHalfOE, 2, MidpointRounding.AwayFromZero);
        OE = Math.Round(traderInfo.OE, 2, MidpointRounding.AwayFromZero);
        Overall = Math.Round(traderInfo.Overall, 2, MidpointRounding.AwayFromZero);
    }

    public void GetMarginValue(TraderPerformanceOtherSportsDetailModel marginInfo)
    {
        FirstHalfHdp = 100 * marginInfo.FirstHalfHdp;
        Hdp = 100 * marginInfo.Hdp;
        FirstHalfOU = 100 * marginInfo.FirstHalfOU;
        OU = 100 * marginInfo.OU;
        FirstHalfOneXTwo = 100 * marginInfo.FirstHalfOneXTwo;
        OneXTwo = 100 * marginInfo.OneXTwo;
        FirstHalfOE = 100 * marginInfo.FirstHalfOE;
        OE = 100 * marginInfo.OE;
        Overall = 100 * marginInfo.Overall;
    }
}