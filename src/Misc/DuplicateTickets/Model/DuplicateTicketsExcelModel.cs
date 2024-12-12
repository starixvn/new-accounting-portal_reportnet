namespace DuplicateTickets.Model
{
    public class DuplicateTicketsExcelModel
    {
        public List<DuplicateTicketsByUserNameExcelModel> Rows { get; set; } = [];

        public string MD5Hash { get; set; } = string.Empty;

        public bool HaveDuplicateTickets { get; set; }
    }
}
