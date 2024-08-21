namespace SuperBalance.Model
{
    public class SuperBalanceModel
    {
        public bool HasData { get; set; }

        public List<BalanceItemModel> SuperBalanceItems { get; set; }

        public BalanceItemModel BaseBalanceTotal { get; set; }
    }
}
