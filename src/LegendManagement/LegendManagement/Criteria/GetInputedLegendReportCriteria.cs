using Fanex.Data.Repository;

namespace LegendManagement.Criteria;

public class GetInputedLegendReportCriteria : CriteriaBase
{
    public GetInputedLegendReportCriteria()
    {
        System = string.Empty;
    }

    public string System { get; set; }

    public override string GetSettingKey() => "LegendManagement_GetInputedLegend";

    public override bool IsValid() => true;
}