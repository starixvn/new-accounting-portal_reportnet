namespace PeakConcurrentUsers.Model
{
    public class PeakConcurrentUser
    {
        public List<ConcurrentUser> ListConcurrentUser { get; set; } = [];

        public string AverageUsers { get; set; }

        public string Total { get; set; }

        public string MaxUsers { get; set; }

        public string MinUsers { get; set; }
    }
}
