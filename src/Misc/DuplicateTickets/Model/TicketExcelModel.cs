using Epplus.Mapper.Annotations;

namespace DuplicateTickets.Model;

public class TicketExcelModel
{
    [Cell("I4")]
    public string Status { get; set; }

    [Cell("J4")]
    public string ReferenceNumber { get; set; }

    [Cell("K4")]
    public string BetTime { get; set; }

    [Cell("L4")]
    public double TimeDifference { get; set; }

    public int GroupNo { get; set; }

    [Cell("O4")]
    public string TransDesc { get; set; }

    [Cell("M4")]
    public string Site { get; set; }

    [Cell("N4")]
    public string BetFrom { get; set; }
}