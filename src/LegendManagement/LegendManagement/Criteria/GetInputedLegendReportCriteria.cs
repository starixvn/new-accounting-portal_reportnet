using Fanex.Data.Repository;

namespace LegendManagement.Criteria;

public class GetInputedLegendReportCriteria : CriteriaBase
{
    public string System { get; set; } = string.Empty;

    public override string GetSettingKey() => "LegendManagement_GetInputedLegend";

    public override bool IsValid() => true;
}