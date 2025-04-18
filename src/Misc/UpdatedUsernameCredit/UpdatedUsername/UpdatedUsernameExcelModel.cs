namespace UpdatedUsernameCredit.UpdatedUsername;

using Epplus.Mapper.Annotations;

public class UpdatedUsernameExcelModel
{
    [Cell("A5")]
    public int Index { get; set; }

    [Cell("B5")]
    public int Year { get; set; }

    [Cell("C5")]
    public int Month { get; set; }

    [Cell("D5")]
    public DateTime LogDate { get; set; }

    [Cell("E5")]
    public string Department { get; set; }

    [Cell("F5")]
    public string Admin { get; set; }

    [Cell("G5")]
    public string CurrentUsername { get; set; }

    [Cell("H5")]
    public string Level { get; set; }

    [Cell("I5")]
    public string Headline { get; set; }

    [Cell("J5")]
    public string Super { get; set; }

    [Cell("K5")]
    public string Master { get; set; }

    [Cell("L5")]
    public string Agent { get; set; }

    [Cell("M5")]
    public string FromUsername { get; set; }

    [Cell("N5")]
    public string ToUsername { get; set; }
}