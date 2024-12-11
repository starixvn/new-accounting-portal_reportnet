namespace DuplicateTickets.Model
{
    public class DuplicateTicketsExcelModel
    {
        public DuplicateTicketsExcelModel()
        {
            Rows = new List<DuplicateTicketsByUserNameExcelModel>();
            MD5Hash = string.Empty;
        }

        public List<DuplicateTicketsByUserNameExcelModel> Rows { get; set; }

        public string MD5Hash { get; set; }

        public bool HaveDuplicateTickets { get; set; }
    }
}
