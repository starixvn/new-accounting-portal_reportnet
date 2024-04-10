namespace HouseWinLoss.Products
{
    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public int PermissionProfileID { get; set; }

        public bool IsNoDraw { get; set; }
    }
}