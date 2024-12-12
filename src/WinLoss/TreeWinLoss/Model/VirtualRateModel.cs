namespace TreeWinLoss.Model
{
    public class VirtualRateModel
    {
        public bool EnableVirtualRate { get; set; }

        public string Date { get; set; }

        public string Sport { get; set; }

        public string LiveStatus { get; set; }

        public List<VirtualRateByBettypeModel> VirtualRateByBettype { get; set; } = [];

        public string ChangedBy { get; set; }

        public int NumberMergeRows { get; set; }

        public List<VirtualRateModel> ReportRows { get; set; } = [];

        public void CalculateNumberMergeRows()
        {
            var numberMergeRows = 0;
            foreach (var row in ReportRows)
            {
                numberMergeRows += row.VirtualRateByBettype.Count;
            }

            NumberMergeRows = numberMergeRows;
        }
    }
}
