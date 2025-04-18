namespace TraderPerformance.Model;

using Epplus.Mapper.Annotations;

public class TraderPerformanceSoccerDetailExcelModel
{
    [Cell("A6")]
    public int No { get; set; }

    [Cell("B6")]
    public string Name { get; set; }

    [Cell("C6")]
    public string EventDate { get; set; }

    [Cell("D6")]
    public string League { get; set; }

    [Cell("E6")]
    public string Match { get; set; }

    [Cell("F6")]
    public decimal? MaxBet { get; set; }

    [Cell("G6")]
    public string HaftTime { get; set; }

    [Cell("H6")]
    public string FullTime { get; set; }

    [Cell("I6")]
    public decimal? WinLossFirstHalfOneXTwo { get; set; }

    [Cell("J6")]
    public decimal? WinLossOneXTwo { get; set; }

    [Cell("K6")]
    public decimal? WinLossFifteenMinOneXTwo { get; set; }

    [Cell("L6")]
    public decimal? WinLossTenMinOneXTwo { get; set; }

    [Cell("M6")]
    public decimal? WinLossFirstHalfHdp { get; set; }

    [Cell("N6")]
    public decimal? WinLossHdp { get; set; }

    [Cell("O6")]
    public decimal? WinLossFifteenMinHdp { get; set; }

    [Cell("P6")]
    public decimal? WinLossTenMinHdp { get; set; }

    [Cell("Q6")]
    public decimal? WinLossFirstHalfOU { get; set; }

    [Cell("R6")]
    public decimal? WinLossOU { get; set; }

    [Cell("S6")]
    public decimal? WinLossFifteenMinOU { get; set; }

    [Cell("T6")]
    public decimal? WinLossTenMinOU { get; set; }

    [Cell("U6")]
    public decimal? WinLossSingleTeamOU { get; set; }

    [Cell("V6")]
    public decimal? WinLossFirstHalfNoOfCorners { get; set; }

    [Cell("W6")]
    public decimal? WinLossNoOfCorners { get; set; }

    [Cell("X6")]
    public decimal? WinLossFifteenMinNoOfCorners { get; set; }

    [Cell("Y6")]
    public decimal? WinLossNextCorner { get; set; }

    [Cell("Z6")]
    public decimal? WinLossFirstHalfTotalBookings { get; set; }

    [Cell("AA6")]
    public decimal? WinLossTotalBookings { get; set; }

    [Cell("AB6")]
    public decimal? WinLossTotalBookingsFifteenMinOU { get; set; }

    [Cell("AC6")]
    public decimal? WinLossTotalBookingsFifteenMinHDP { get; set; }

    [Cell("AD6")]
    public decimal? WinLossNextBookings { get; set; }

    [Cell("AE6")]
    public decimal? WinLossOverall { get; set; }

    [Cell("AF6")]
    public decimal? TurnoverFirstHalfOneXTwo { get; set; }

    [Cell("AG6")]
    public decimal? TurnoverOneXTwo { get; set; }

    [Cell("AH6")]
    public decimal? TurnoverFifteenMinOneXTwo { get; set; }

    [Cell("AI6")]
    public decimal? TurnoverTenMinOneXTwo { get; set; }

    [Cell("AJ6")]
    public decimal? TurnoverFirstHalfHdp { get; set; }

    [Cell("AK6")]
    public decimal? TurnoverHdp { get; set; }

    [Cell("AL6")]
    public decimal? TurnoverFifteenMinHdp { get; set; }

    [Cell("AM6")]
    public decimal? TurnoverTenMinHdp { get; set; }

    [Cell("AN6")]
    public decimal? TurnoverFirstHalfOU { get; set; }

    [Cell("AO6")]
    public decimal? TurnoverOU { get; set; }

    [Cell("AP6")]
    public decimal? TurnoverFifteenMinOU { get; set; }

    [Cell("AQ6")]
    public decimal? TurnoverTenMinOU { get; set; }

    [Cell("AR6")]
    public decimal? TurnoverSingleTeamOU { get; set; }

    [Cell("AS6")]
    public decimal? TurnoverFirstHalfNoOfCorners { get; set; }

    [Cell("AT6")]
    public decimal? TurnoverNoOfCorners { get; set; }

    [Cell("AU6")]
    public decimal? TurnoverFifteenMinNoOfCorners { get; set; }

    [Cell("AV6")]
    public decimal? TurnoverNextCorner { get; set; }

    [Cell("AW6")]
    public decimal? TurnoverFirstHalfTotalBookings { get; set; }

    [Cell("AX6")]
    public decimal? TurnoverTotalBookings { get; set; }

    [Cell("AY6")]
    public decimal? TurnoverTotalBookingsFifteenMinOU { get; set; }

    [Cell("AZ6")]
    public decimal? TurnoverTotalBookingsFifteenMinHDP { get; set; }

    [Cell("BA6")]
    public decimal? TurnoverNextBookings { get; set; }

    [Cell("BB6")]
    public decimal? TurnoverOverall { get; set; }

    [Cell("BC6")]
    public decimal? LiveBetCountFirstHalfOneXTwo { get; set; }

    [Cell("BD6")]
    public decimal? LiveBetCountOneXTwo { get; set; }

    [Cell("BE6")]
    public decimal? LiveBetCountFifteenMinOneXTwo { get; set; }

    [Cell("BF6")]
    public decimal? LiveBetCountTenMinOneXTwo { get; set; }

    [Cell("BG6")]
    public decimal? LiveBetCountFirstHalfHdp { get; set; }

    [Cell("BH6")]
    public decimal? LiveBetCountHdp { get; set; }

    [Cell("BI6")]
    public decimal? LiveBetCountFifteenMinHdp { get; set; }

    [Cell("BJ6")]
    public decimal? LiveBetCountTenMinHdp { get; set; }

    [Cell("BK6")]
    public decimal? LiveBetCountFirstHalfOU { get; set; }

    [Cell("BL6")]
    public decimal? LiveBetCountOU { get; set; }

    [Cell("BM6")]
    public decimal? LiveBetCountFifteenMinOU { get; set; }

    [Cell("BN6")]
    public decimal? LiveBetCountTenMinOU { get; set; }

    [Cell("BO6")]
    public decimal? LiveBetCountSingleTeamOU { get; set; }

    [Cell("BP6")]
    public decimal? LiveBetCountFirstHalfNoOfCorners { get; set; }

    [Cell("BQ6")]
    public decimal? LiveBetCountNoOfCorners { get; set; }

    [Cell("BR6")]
    public decimal? LiveBetCountFifteenMinNoOfCorners { get; set; }

    [Cell("BS6")]
    public decimal? LiveBetCountNextCorner { get; set; }

    [Cell("BT6")]
    public decimal? LiveBetCountFirstHalfTotalBookings { get; set; }

    [Cell("BU6")]
    public decimal? LiveBetCountTotalBookings { get; set; }

    [Cell("BV6")]
    public decimal? LiveBetCountTotalBookingsFifteenMinOU { get; set; }

    [Cell("BW6")]
    public decimal? LiveBetCountTotalBookingsFifteenMinHDP { get; set; }

    [Cell("BX6")]
    public decimal? LiveBetCountNextBookings { get; set; }

    [Cell("BY6")]
    public decimal? LiveBetCountOverall { get; set; }

    [Cell("BZ6")]
    public decimal? MarginFirstHalfOneXTwo { get; set; }

    [Cell("CA6")]
    public decimal? MarginOneXTwo { get; set; }

    [Cell("CB6")]
    public decimal? MarginFifteenMinOneXTwo { get; set; }

    [Cell("CC6")]
    public decimal? MarginTenMinOneXTwo { get; set; }

    [Cell("CD6")]
    public decimal? MarginFirstHalfHdp { get; set; }

    [Cell("CE6")]
    public decimal? MarginHdp { get; set; }

    [Cell("CF6")]
    public decimal? MarginFifteenMinHdp { get; set; }

    [Cell("CG6")]
    public decimal? MarginTenMinHdp { get; set; }

    [Cell("CH6")]
    public decimal? MarginFirstHalfOU { get; set; }

    [Cell("CI6")]
    public decimal? MarginOU { get; set; }

    [Cell("CJ6")]
    public decimal? MarginFifteenMinOU { get; set; }

    [Cell("CK6")]
    public decimal? MarginTenMinOU { get; set; }

    [Cell("CL6")]
    public decimal? MarginSingleTeamOU { get; set; }

    [Cell("CM6")]
    public decimal? MarginFirstHalfNoOfCorners { get; set; }

    [Cell("CN6")]
    public decimal? MarginNoOfCorners { get; set; }

    [Cell("CO6")]
    public decimal? MarginFifteenMinNoOfCorners { get; set; }

    [Cell("CP6")]
    public decimal? MarginNextCorner { get; set; }

    [Cell("CQ6")]
    public decimal? MarginFirstHalfTotalBookings { get; set; }

    [Cell("CR6")]
    public decimal? MarginTotalBookings { get; set; }

    [Cell("CS6")]
    public decimal? MarginTotalBookingsFifteenMinOU { get; set; }

    [Cell("CT6")]
    public decimal? MarginTotalBookingsFifteenMinHDP { get; set; }

    [Cell("CU6")]
    public decimal? MarginNextBookings { get; set; }

    [Cell("CV6")]
    public decimal? MarginOverall { get; set; }

    [Cell("CW6")]
    public decimal? CommissionFirstHalfOneXTwo { get; set; }

    [Cell("CX6")]
    public decimal? CommissionOneXTwo { get; set; }

    [Cell("CY6")]
    public decimal? CommissionFifteenMinOneXTwo { get; set; }

    [Cell("CZ6")]
    public decimal? CommissionTenMinOneXTwo { get; set; }

    [Cell("DA6")]
    public decimal? CommissionFirstHalfHdp { get; set; }

    [Cell("DB6")]
    public decimal? CommissionHdp { get; set; }

    [Cell("DC6")]
    public decimal? CommissionFifteenMinHdp { get; set; }

    [Cell("DD6")]
    public decimal? CommissionTenMinHdp { get; set; }

    [Cell("DE6")]
    public decimal? CommissionFirstHalfOU { get; set; }

    [Cell("DF6")]
    public decimal? CommissionOU { get; set; }

    [Cell("DG6")]
    public decimal? CommissionFifteenMinOU { get; set; }

    [Cell("DH6")]
    public decimal? CommissionTenMinOU { get; set; }

    [Cell("DI6")]
    public decimal? CommissionSingleTeamOU { get; set; }

    [Cell("DJ6")]
    public decimal? CommissionFirstHalfNoOfCorners { get; set; }

    [Cell("DK6")]
    public decimal? CommissionNoOfCorners { get; set; }

    [Cell("DL6")]
    public decimal? CommissionFifteenMinNoOfCorners { get; set; }

    [Cell("DM6")]
    public decimal? CommissionNextCorner { get; set; }

    [Cell("DN6")]
    public decimal? CommissionFirstHalfTotalBookings { get; set; }

    [Cell("DO6")]
    public decimal? CommissionTotalBookings { get; set; }

    [Cell("DP6")]
    public decimal? CommissionTotalBookingsFifteenMinOU { get; set; }

    [Cell("DQ6")]
    public decimal? CommissionTotalBookingsFifteenMinHDP { get; set; }

    [Cell("DR6")]
    public decimal? CommissionNextBookings { get; set; }

    [Cell("DS6")]
    public decimal? CommissionOverall { get; set; }
}