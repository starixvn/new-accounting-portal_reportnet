namespace TreeWinLoss.Model
{
    public class VirtualRateModel
    {
        public VirtualRateModel()
        {
            ReportRows = new List<VirtualRateModel>();
            VirtualRateByBettype = new List<VirtualRateByBettypeModel>();
        }

        public bool EnableVirtualRate { get; set; }

        public string Date { get; set; }

        public string Sport { get; set; }

        public string LiveStatus { get; set; }

        public List<VirtualRateByBettypeModel> VirtualRateByBettype { get; set; }

        public string ChangedBy { get; set; }

        public int NumberMergeRows { get; set; }

        public List<VirtualRateModel> ReportRows { get; set; }

        public void CalculateNumberMergeRows()
        {
            int numberMergeRows = 0;
            foreach (var row in this.ReportRows)
            {
                numberMergeRows += row.VirtualRateByBettype.Count;
            }

            NumberMergeRows = numberMergeRows;
        }
    }
}
