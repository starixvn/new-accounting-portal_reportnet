namespace SuperBalance.Model
{
    public class BalanceItemModel
    {
        public string CustId { get; set; }

        public string Username { get; set; }

        public string Currency { get; set; }

        public bool HeadlineEnabled { get; set; }

        public decimal Balance { get; set; }

        public decimal BaseBalance { get; set; }

        public void AddToBaseBalance(BalanceItemModel item)
        {
            if (item == null)
            {
                return;
            }

            this.BaseBalance += item.BaseBalance;
        }
    }
}
