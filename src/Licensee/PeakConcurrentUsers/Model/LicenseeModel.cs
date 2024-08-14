namespace PeakConcurrentUsers.Model
{
    public class LicenseeModel
    {
        public int ResellerId { get; set; }

        public string ResellerName { get; set; }

        public int ResellerType { get; set; }

        public int LicenseeId { get; set; }

        public string LicenseeName { get; set; }

        public bool IsEnabledBA { get; set; }

        public string BrandName { get; set; }

        public bool IsMultiAgent => LicenseeName == "12Bet";
    }
}
