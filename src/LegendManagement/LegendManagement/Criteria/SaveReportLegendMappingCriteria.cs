namespace LegendManagement.Criteria;

using Fanex.Data.Repository;

public class SaveReportLegendMappingCriteria : CriteriaBase, INonQueryCommand
{
    public int AccMenuId { get; set; }

    public string NewSystem { get; set; }

    public int NewMenuId { get; set; }

    public int LegendId { get; set; }

    public bool IsNote { get; set; }

    public string Errmsg { get; set; }

    public override string GetSettingKey() => "LegendManagement_SaveReportLegendMapping";

    public override bool IsValid() => true;
}