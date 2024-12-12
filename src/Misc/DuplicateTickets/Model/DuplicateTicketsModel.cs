namespace DuplicateTickets.Model
{
    public class DuplicateTicketsModel
    {
        public List<DuplicateTicketsByUserNameModel> Rows { get; set; } = [];

        public string MD5Hash { get; set; } = string.Empty;

        public bool HaveDuplicateTickets { get; set; }
    }
}
