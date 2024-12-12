using Epplus.Mapper.Annotations;

namespace AbnormalBet.Model
{
    public class MemberExcelModel
    {
        [Cell("A5")]
        public string RefNo { get; set; }

        [Cell("B5")]
        public string Sport { get; set; }

        [Cell("C5")]
        public string League { get; set; }

        [Cell("D5")]
        public string Match { get; set; }

        [Cell("E5")]
        public string Reason { get; set; }

        public decimal Odds { get; set; }

        [Cell("F5")]
        public dynamic OddsData => Sport == "-" ? "-" : Odds;

        [Cell("G5")]
        public string Currency { get; set; }

        [Cell("H5")]
        public decimal Stake { get; set; }

        public decimal HDP { get; set; }

        [Cell("I5")]
        public dynamic HDPData => Sport == "-" ? "-" : HDP;

        [Cell("J5")]
        public string Score { get; set; }

        [Cell("K5")]
        public string TransTime { get; set; }

        [Cell("L5")]
        public string IP { get; set; }

        [Cell("M5")]
        public string Location { get; set; }
    }
}
