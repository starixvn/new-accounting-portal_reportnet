namespace AbnormalBet.Model
{
    public class LicenseeModel
    {
        public IEnumerable<LicenseeItemModel> Licensees { get; set; }

        public int TotalItems { get; set; }
    }
}