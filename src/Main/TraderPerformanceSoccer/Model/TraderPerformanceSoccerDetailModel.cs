namespace TraderPerformance.Model
{
    public class TraderPerformanceSoccerDetailModel
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

        public decimal FifteenMinOU { get; set; }

        public decimal TenMinOU { get; set; }

        public decimal SingleTeamOU { get; set; }        

        public decimal FirstHalfHdp { get; set; }

        public decimal Hdp { get; set; }

        public decimal FifteenMinHdp { get; set; }

        public decimal FirstHalfOneXTwo { get; set; }

        public decimal OneXTwo { get; set; }

        public decimal FifteenMinOneXTwo { get; set; }

        public decimal FirstHalfNoOfCorners { get; set; }

        public decimal NoOfCorners { get; set; }

        public decimal FifteenMinNoOfCorners { get; set; }

        public decimal NextCorner { get; set; }

        public decimal FirstHalfTotalBookings { get; set; }

        public decimal TotalBookings { get; set; }

        public decimal TotalBookingsFifteenMinOU { get; set; }

        public decimal TotalBookingsFifteenMinHDP { get; set; }

        public decimal NextBookings { get; set; }

        public decimal Overall { get; set; }

        public decimal Maxbet { get; set; }

        public decimal TenMinHdp { get; set; }

        public decimal TenMinOneXTwo { get; set; }

        public List<TraderPerformanceSoccerDetailModel> ChildInfo { get; set; } = [];

        public List<TraderPerformanceSoccerDetailModel> ReportRows { get; set; } = [];

        public TraderPerformanceSoccerDetailModel GrandTotal { get; set; }

        public int ReportLength { get; set; }

        public void CalculateTotal(TraderPerformanceSoccerDetailModel traderInfo)
        {
            FirstHalfHdp += traderInfo.FirstHalfHdp;
            Hdp += traderInfo.Hdp;
            FifteenMinHdp += traderInfo.FifteenMinHdp;
            FirstHalfOU += traderInfo.FirstHalfOU;
            OU += traderInfo.OU;
            FirstHalfOneXTwo += traderInfo.FirstHalfOneXTwo;
            OneXTwo += traderInfo.OneXTwo;
            FifteenMinOneXTwo += traderInfo.FifteenMinOneXTwo;
            FirstHalfNoOfCorners += traderInfo.FirstHalfNoOfCorners;
            FifteenMinNoOfCorners += traderInfo.FifteenMinNoOfCorners;
            NoOfCorners += traderInfo.NoOfCorners;
            FifteenMinOU += traderInfo.FifteenMinOU;
            TenMinOU += traderInfo.TenMinOU;
            SingleTeamOU += traderInfo.SingleTeamOU;
            FirstHalfTotalBookings += traderInfo.FirstHalfTotalBookings;
            TotalBookings += traderInfo.TotalBookings;
            TotalBookingsFifteenMinOU += traderInfo.TotalBookingsFifteenMinOU;
            TotalBookingsFifteenMinHDP += traderInfo.TotalBookingsFifteenMinHDP;
            NextBookings += traderInfo.NextBookings;            
            NextCorner += traderInfo.NextCorner;
            Overall += traderInfo.Overall;
            TenMinHdp += traderInfo.TenMinHdp;
            TenMinOneXTwo += traderInfo.TenMinOneXTwo;
        }

        
        public void CalculateMarginTotal(TraderPerformanceSoccerDetailModel winLossInfo, TraderPerformanceSoccerDetailModel turnOverInfo)
        {
            FirstHalfHdp = turnOverInfo.FirstHalfHdp == 0 ?
                0 :
                winLossInfo.FirstHalfHdp / turnOverInfo.FirstHalfHdp * 100;
            Hdp = turnOverInfo.Hdp == 0 ?
                0 :
                winLossInfo.Hdp / turnOverInfo.Hdp * 100;
            FifteenMinHdp = turnOverInfo.FifteenMinHdp == 0 ?
                0 :
                winLossInfo.FifteenMinHdp / turnOverInfo.FifteenMinHdp * 100;
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
            FifteenMinOneXTwo = turnOverInfo.FifteenMinOneXTwo == 0 ?
                0 :
                winLossInfo.FifteenMinOneXTwo / turnOverInfo.FifteenMinOneXTwo * 100;
            FirstHalfNoOfCorners = turnOverInfo.FirstHalfNoOfCorners == 0 ?
                0 :
                winLossInfo.FirstHalfNoOfCorners / turnOverInfo.FirstHalfNoOfCorners * 100;
            FifteenMinNoOfCorners = turnOverInfo.FifteenMinNoOfCorners == 0 ?
                0 :
                winLossInfo.FifteenMinNoOfCorners / turnOverInfo.FifteenMinNoOfCorners * 100;
            NoOfCorners = turnOverInfo.NoOfCorners == 0 ?
                0 :
                winLossInfo.NoOfCorners / turnOverInfo.NoOfCorners * 100;
            FifteenMinOU = turnOverInfo.FifteenMinOU == 0 ?
                0 :
                winLossInfo.FifteenMinOU / turnOverInfo.FifteenMinOU * 100;
            TenMinOU = turnOverInfo.TenMinOU == 0 ?
                0 :
                winLossInfo.TenMinOU / turnOverInfo.TenMinOU * 100;
            SingleTeamOU = turnOverInfo.SingleTeamOU == 0 ?
                0 :
                winLossInfo.SingleTeamOU / turnOverInfo.SingleTeamOU * 100;
            FirstHalfTotalBookings = turnOverInfo.FirstHalfTotalBookings == 0 ?
                0 :
                winLossInfo.FirstHalfTotalBookings / turnOverInfo.FirstHalfTotalBookings * 100;
            TotalBookings = turnOverInfo.TotalBookings == 0 ?
                0 :
                winLossInfo.TotalBookings / turnOverInfo.TotalBookings * 100;
            TotalBookingsFifteenMinOU = turnOverInfo.TotalBookingsFifteenMinOU == 0 ?
                0 :
                winLossInfo.TotalBookingsFifteenMinOU / turnOverInfo.TotalBookingsFifteenMinOU * 100;
            TotalBookingsFifteenMinHDP = turnOverInfo.TotalBookingsFifteenMinHDP == 0 ?
                0 :
                winLossInfo.TotalBookingsFifteenMinHDP / turnOverInfo.TotalBookingsFifteenMinHDP * 100;
            NextBookings = turnOverInfo.NextBookings == 0 ?
                0 :
                winLossInfo.NextBookings / turnOverInfo.NextBookings * 100;            
            NextCorner = turnOverInfo.NextCorner == 0 ?
                0 :
                winLossInfo.NextCorner / turnOverInfo.NextCorner * 100;
            Overall = turnOverInfo.Overall == 0 ?
                0 :
                winLossInfo.Overall / turnOverInfo.Overall * 100;
            TenMinHdp = turnOverInfo.TenMinHdp == 0 ?
                0 :
                winLossInfo.TenMinHdp / turnOverInfo.TenMinHdp * 100;
            TenMinOneXTwo = turnOverInfo.TenMinOneXTwo == 0 ?
                0 :
                winLossInfo.TenMinOneXTwo / turnOverInfo.TenMinOneXTwo * 100; 
        }

        public void FormatTwoDecimalNumberData(TraderPerformanceSoccerDetailModel traderInfo)
        {
            FirstHalfHdp = Math.Round(traderInfo.FirstHalfHdp, 2, MidpointRounding.AwayFromZero);
            Hdp = Math.Round(traderInfo.Hdp, 2, MidpointRounding.AwayFromZero);
            FifteenMinHdp = Math.Round(traderInfo.FifteenMinHdp, 2, MidpointRounding.AwayFromZero);
            FirstHalfOU = Math.Round(traderInfo.FirstHalfOU, 2, MidpointRounding.AwayFromZero);
            OU = Math.Round(traderInfo.OU, 2, MidpointRounding.AwayFromZero);
            FirstHalfOneXTwo = Math.Round(traderInfo.FirstHalfOneXTwo, 2, MidpointRounding.AwayFromZero);
            OneXTwo = Math.Round(traderInfo.OneXTwo, 2, MidpointRounding.AwayFromZero);
            FifteenMinOneXTwo = Math.Round(traderInfo.FifteenMinOneXTwo, 2, MidpointRounding.AwayFromZero);
            FirstHalfNoOfCorners = Math.Round(traderInfo.FirstHalfNoOfCorners, 2, MidpointRounding.AwayFromZero);
            FifteenMinNoOfCorners = Math.Round(traderInfo.FifteenMinNoOfCorners, 2, MidpointRounding.AwayFromZero);
            NoOfCorners = Math.Round(traderInfo.NoOfCorners, 2, MidpointRounding.AwayFromZero);
            FifteenMinOU = Math.Round(traderInfo.FifteenMinOU, 2, MidpointRounding.AwayFromZero);
            TenMinOU = Math.Round(traderInfo.TenMinOU, 2, MidpointRounding.AwayFromZero);
            SingleTeamOU = Math.Round(traderInfo.SingleTeamOU, 2, MidpointRounding.AwayFromZero);
            FirstHalfTotalBookings = Math.Round(traderInfo.FirstHalfTotalBookings, 2, MidpointRounding.AwayFromZero);
            TotalBookings = Math.Round(traderInfo.TotalBookings, 2, MidpointRounding.AwayFromZero);
            TotalBookingsFifteenMinOU = Math.Round(traderInfo.TotalBookingsFifteenMinOU, 2, MidpointRounding.AwayFromZero);
            TotalBookingsFifteenMinHDP = Math.Round(traderInfo.TotalBookingsFifteenMinHDP, 2, MidpointRounding.AwayFromZero);
            NextBookings = Math.Round(traderInfo.NextBookings, 2, MidpointRounding.AwayFromZero);            
            NextCorner = Math.Round(traderInfo.NextCorner, 2, MidpointRounding.AwayFromZero);
            Overall = Math.Round(traderInfo.Overall, 2, MidpointRounding.AwayFromZero);
            TenMinHdp = Math.Round(traderInfo.TenMinHdp, 2, MidpointRounding.AwayFromZero);
            TenMinOneXTwo = Math.Round(traderInfo.TenMinOneXTwo, 2, MidpointRounding.AwayFromZero); 
        }

        public void GetMarginValue(TraderPerformanceSoccerDetailModel marginInfo)
        {
            FirstHalfHdp = 100 * marginInfo.FirstHalfHdp;
            Hdp = 100 * marginInfo.Hdp;
            FifteenMinHdp = 100 * marginInfo.FifteenMinHdp;
            FirstHalfOU = 100 * marginInfo.FirstHalfOU;
            OU = 100 * marginInfo.OU;
            FirstHalfOneXTwo = 100 * marginInfo.FirstHalfOneXTwo;
            OneXTwo = 100 * marginInfo.OneXTwo;
            FifteenMinOneXTwo = 100 * marginInfo.FifteenMinOneXTwo;
            FirstHalfNoOfCorners = 100 * marginInfo.FirstHalfNoOfCorners;
            FifteenMinNoOfCorners = 100 * marginInfo.FifteenMinNoOfCorners;
            NoOfCorners = 100 * marginInfo.NoOfCorners;
            FifteenMinOU = 100 * marginInfo.FifteenMinOU;
            TenMinOU = 100 * marginInfo.TenMinOU;
            SingleTeamOU = 100 * marginInfo.SingleTeamOU;
            FirstHalfTotalBookings = 100 * marginInfo.FirstHalfTotalBookings;
            TotalBookings = 100 * marginInfo.TotalBookings;
            TotalBookingsFifteenMinOU = 100 * marginInfo.TotalBookingsFifteenMinOU;
            TotalBookingsFifteenMinHDP = 100 * marginInfo.TotalBookingsFifteenMinHDP;
            NextBookings = 100 * marginInfo.NextBookings;            
            NextCorner = 100 * marginInfo.NextCorner;
            Overall = 100 * marginInfo.Overall;
            TenMinOneXTwo = 100 * marginInfo.TenMinOneXTwo;
            TenMinHdp = 100 * marginInfo.TenMinHdp; 
        }
    }
}
