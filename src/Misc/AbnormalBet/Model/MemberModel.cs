namespace AbnormalBet.Model
{
    public class MemberModel
    {
        public IEnumerable<MemberItemModel> Members { get; set; }

        public int TotalItems { get; set; }
    }
}