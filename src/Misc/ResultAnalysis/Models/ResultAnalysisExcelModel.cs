using Epplus.Mapper.Annotations;

namespace ResultAnalysis.Models
{
    using System.Globalization;

    public class ResultAnalysisExcelModel
	{
		public IEnumerable<DetailedResultAnalysisExcelModel> Items { get; set; }

		public ResultAnalysisSummaryExcelModel Summary { get; set; }
	}

	public class ResultAnalysisSummaryExcelModel
	{
		[Cell("A6:B6")]
		public int TotalLeague { get; set; }

		[Cell("C6")]
		public int TotalMatch { get; set; }

		[Cell("D6")]
		public int FavHdpResult { get; set; }

		[Cell("E6")]
		public int UnderdogHdpResult { get; set; }

		[Cell("F6")]
		public int DrawHdpResult { get; set; }

		[Cell("G6")]
		public int OverOuResult { get; set; }

		[Cell("H6")]
		public int UnderOuResult { get; set; }

		[Cell("I6")]
		public int DrawOuResult { get; set; }

		[Cell("J6")]
		public int Fav1x2Result { get; set; }

		[Cell("K6")]
		public int Underdog1x2Result { get; set; }

		[Cell("L6")]
		public int Draw1x2Result { get; set; }

		[Cell("M6")]
		public int TgResult01 { get; set; }

		[Cell("N6")]
		public int TgResult23 { get; set; }

		[Cell("O6")]
		public int TgResult46 { get; set; }

		[Cell("P6")]
		public int TgResult7 { get; set; }
	}

	public class DetailedResultAnalysisExcelModel
	{
		[Cell("A10")]
		public DateTime Date { get; set; }

		[Cell("B10")]
		public string League { get; set; }

		[Cell("C10")]
		public string Match { get; set; }

        public string HDP { get; set; }

        [Cell("D10")]
        public string HdpString => HDP == "-" ? "-" : Math.Round(decimal.Parse(HDP), 2).ToString(CultureInfo.CurrentCulture);

        public string OU { get; set; }

        [Cell("E10")]
		public string OuString => OU == "-" ? "-" : Math.Round(decimal.Parse(OU), 2).ToString(CultureInfo.CurrentCulture);

        [Cell("F10")]
		public string Fav { get; set; }

		[Cell("G10")]
		public string HtScoreHome { get; set; }

		[Cell("H10")]
		public string HtScoreAway { get; set; }

		[Cell("I10")]
		public string FtScoreHome { get; set; }

		[Cell("J10")]
		public string FtScoreAway { get; set; }

		[Cell("K10")]
		public string ResultHDP { get; set; }

		[Cell("L10")]
		public string ResultOU { get; set; }

		[Cell("M10")]
		public string Result1x2 { get; set; }

		[Cell("N10")]
		public string ResultTG { get; set; }
	}
}
