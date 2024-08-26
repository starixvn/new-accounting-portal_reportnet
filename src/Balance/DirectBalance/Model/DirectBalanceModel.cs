namespace DirectBalance.Model
{
    public class DirectBalanceModel
    {
        public IEnumerable<DirectBalanceItemModel> DirectBalanceItems{ get; set; }

        public DirectBalanceItemModel Total { get;set; }
    }
}
