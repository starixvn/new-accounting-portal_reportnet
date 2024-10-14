namespace TraderPerformance.Model
{
    public class TraderPerformanceOtherSportsModel
    {
        public TraderPerformanceOtherSportsModel()
        {
            ReportRows = new List<TraderPerformanceOtherSportsModel>();
            ChildInfo = new List<TraderPerformanceOtherSportsModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MatchNumber { get; set; }

        public int Major { get; set; }

        public int Minor1 { get; set; }

        public int Minor2 { get; set; }

        public int Minor3 { get; set; }

        public int Others { get; set; }

        public decimal FirstHalfOU { get; set; }

        public decimal OU { get; set; }

        public decimal FirstHalfOE { get; set; }

        public decimal OE { get; set; }

        public decimal FirstHalfHdp { get; set; }

        public decimal Hdp { get; set; }

        public decimal FirstHalfOneXTwo { get; set; }

        public decimal OneXTwo { get; set; }

        public decimal Overall { get; set; }

        public List<TraderPerformanceOtherSportsModel> ChildInfo { get; set; }

        public List<TraderPerformanceOtherSportsModel> ReportRows { get; set; }

        public TraderPerformanceOtherSportsModel GrandTotal { get; set; }

        public void CalculateTotal(TraderPerformanceOtherSportsModel traderInfo)
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

        
        public void CalculateMarginTotal(TraderPerformanceOtherSportsModel winLossInfo, TraderPerformanceOtherSportsModel turnOverInfo)
        {
            FirstHalfHdp = turnOverInfo.FirstHalfHdp == 0 ?
                0 :
                (winLossInfo.FirstHalfHdp / turnOverInfo.FirstHalfHdp) * 100;
            Hdp = turnOverInfo.Hdp == 0 ?
                0 :
                (winLossInfo.Hdp / turnOverInfo.Hdp) * 100;
            FirstHalfOU = turnOverInfo.FirstHalfOU == 0 ?
                0 :
                (winLossInfo.FirstHalfOU / turnOverInfo.FirstHalfOU) * 100;
            OU = turnOverInfo.OU == 0 ?
                0 :
                (winLossInfo.OU / turnOverInfo.OU) * 100;
            FirstHalfOneXTwo = turnOverInfo.FirstHalfOneXTwo == 0 ?
                0 :
                (winLossInfo.FirstHalfOneXTwo / turnOverInfo.FirstHalfOneXTwo) * 100;
            OneXTwo = turnOverInfo.OneXTwo == 0 ?
                0 :
                (winLossInfo.OneXTwo / turnOverInfo.OneXTwo) * 100;
            FirstHalfOE = turnOverInfo.FirstHalfOE == 0 ?
                0 :
                (winLossInfo.FirstHalfOE / turnOverInfo.FirstHalfOE) * 100;
            OE = turnOverInfo.OE == 0 ?
                0 :
                (winLossInfo.OE / turnOverInfo.OE) * 100;
            Overall = turnOverInfo.Overall == 0 ?
                0 :
                (winLossInfo.Overall / turnOverInfo.Overall) * 100;
        }

        public void CalculateTotalMatchAndLeague(
            int matchNumber,
            int major,
            int minor1,
            int minor2,
            int minor3,
            int others)
        {
            MatchNumber += matchNumber;
            Major += major;
            Minor1 += minor1;
            Minor2 += minor2;
            Minor3 += minor3;
            Others += others;
        }

        public void FormatTwoDecimalNumberData(TraderPerformanceOtherSportsModel traderInfo)
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

        public void GetMarginValue(TraderPerformanceOtherSportsModel marginInfo)
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
}
