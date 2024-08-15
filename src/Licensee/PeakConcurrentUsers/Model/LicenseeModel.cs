namespace PeakConcurrentUsers.Model
{
    public class LicenseeModel
    {
        public int LicenseeId { get; set; }

        public string LicenseeName { get; set; }

        public bool IsEnabledBA { get; set; }

        public string BrandName { get; set; }

        public bool IsMultiAgent => LicenseeName == "12Bet";
    }
}
