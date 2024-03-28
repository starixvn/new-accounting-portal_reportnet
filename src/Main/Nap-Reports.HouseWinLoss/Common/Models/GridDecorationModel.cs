namespace Nap_Reports.HouseWinLoss.Common.Models
{
    public class GridDecorationModel
    {
        private const int DefaultMainColumnWidth = 180;

        public GridDecorationModel()
        {
            Title = string.Empty;
            LiveTitle = string.Empty;
            NonLiveTitle = string.Empty;
            MainColumnText = string.Empty;
            MainColumnWidth = DefaultMainColumnWidth;
        }

        public string Title { get; set; }

        public string LiveTitle { get; set; }

        public string NonLiveTitle { get; set; }

        public string MainColumnText { get; set; }

        public int MainColumnWidth { get; set; }

        public string SheetName { get; set; }
    }
}
