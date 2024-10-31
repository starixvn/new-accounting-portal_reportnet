namespace DuplicateTickets.Model
{
    public class DuplicateTicketsModel
    {
        public DuplicateTicketsModel()
        {
            Rows = new List<DuplicateTicketsByUserNameModel>();
            MD5Hash = string.Empty;
        }

        public List<DuplicateTicketsByUserNameModel> Rows { get; set; }

        public string MD5Hash { get; set; }

        public bool HaveDuplicateTickets { get; set; }
    }
}
