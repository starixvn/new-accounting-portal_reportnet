namespace TraderPerformance.Model;

using Epplus.Mapper.Annotations;

public class TraderPerformanceOtherSportsDetailExcelModel
{
    [Cell("A6")]
    public int No { get; set; }

    [Cell("B6")]
    public string Name { get; set; }

    [Cell("C6")]
    public string EventDate { get; set; }

    [Cell("D6")]
    public string LeagueName { get; set; }

    [Cell("E6")]
    public string Match { get; set; }

    [Cell("F6")]
    public decimal Maxbet { get; set; }

    [Cell("G6")]
    public string HaftTime { get; set; }

    [Cell("H6")]
    public string FullTime { get; set; }

    [Cell("I6")]
    public decimal? WinLossFirstHalfOneXTwo { get; set; }

    [Cell("J6")]
    public decimal? WinLossOneXTwo { get; set; }

    [Cell("K6")]
    public decimal? WinLossFirstHalfHdp { get; set; }

    [Cell("L6")]
    public decimal? WinLossHdp { get; set; }

    [Cell("M6")]
    public decimal? WinLossFirstHalfOU { get; set; }

    [Cell("N6")]
    public decimal? WinLossOU { get; set; }

    [Cell("O6")]
    public decimal? WinLossFirstHalfOE { get; set; }

    [Cell("P6")]
    public decimal? WinLossOE { get; set; }

    [Cell("Q6")]
    public decimal? WinLossOverall { get; set; }

    [Cell("R6")]
    public decimal? TurnOverFirstHalfOneXTwo { get; set; }

    [Cell("S6")]
    public decimal? TurnOverOneXTwo { get; set; }

    [Cell("T6")]
    public decimal? TurnOverFirstHalfHdp { get; set; }

    [Cell("U6")]
    public decimal? TurnOverHdp { get; set; }

    [Cell("V6")]
    public decimal? TurnOverFirstHalfOU { get; set; }

    [Cell("W6")]
    public decimal? TurnOverOU { get; set; }

    [Cell("X6")]
    public decimal? TurnOverFirstHalfOE { get; set; }

    [Cell("Y6")]
    public decimal? TurnOverOE { get; set; }

    [Cell("Z6")]
    public decimal? TurnOverOverall { get; set; }

    [Cell("AA6")]
    public decimal? LiveBetCountFirstHalfOneXTwo { get; set; }

    [Cell("AB6")]
    public decimal? LiveBetCountOneXTwo { get; set; }

    [Cell("AC6")]
    public decimal? LiveBetCountFirstHalfHdp { get; set; }

    [Cell("AD6")]
    public decimal? LiveBetCountHdp { get; set; }

    [Cell("AE6")]
    public decimal? LiveBetCountFirstHalfOU { get; set; }

    [Cell("AF6")]
    public decimal? LiveBetCountOU { get; set; }

    [Cell("AG6")]
    public decimal? LiveBetCountFirstHalfOE { get; set; }

    [Cell("AH6")]
    public decimal? LiveBetCountOE { get; set; }

    [Cell("AI6")]
    public decimal? LiveBetCountOverall { get; set; }

    [Cell("AJ6")]
    public decimal? MarginFirstHalfOneXTwo { get; set; }

    [Cell("AK6")]
    public decimal? MarginOneXTwo { get; set; }

    [Cell("AL6")]
    public decimal? MarginFirstHalfHdp { get; set; }

    [Cell("AM6")]
    public decimal? MarginHdp { get; set; }

    [Cell("AN6")]
    public decimal? MarginFirstHalfOU { get; set; }

    [Cell("AO6")]
    public decimal? MarginOU { get; set; }

    [Cell("AP6")]
    public decimal? MarginFirstHalfOE { get; set; }

    [Cell("AQ6")]
    public decimal? MarginOE { get; set; }

    [Cell("AR6")]
    public decimal? MarginOverall { get; set; }

    [Cell("AS6")]
    public decimal? CommissionFirstHalfOneXTwo { get; set; }

    [Cell("AT6")]
    public decimal? CommissionOneXTwo { get; set; }

    [Cell("AU6")]
    public decimal? CommissionFirstHalfHdp { get; set; }

    [Cell("AV6")]
    public decimal? CommissionHdp { get; set; }

    [Cell("AW6")]
    public decimal? CommissionFirstHalfOU { get; set; }

    [Cell("AX6")]
    public decimal? CommissionOU { get; set; }

    [Cell("AY6")]
    public decimal? CommissionFirstHalfOE { get; set; }

    [Cell("AZ6")]
    public decimal? CommissionOE { get; set; }

    [Cell("BA6")]
    public decimal? CommissionOverall { get; set; }
}