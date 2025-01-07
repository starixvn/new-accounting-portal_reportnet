using Fanex.Data.Repository;

namespace LegendManagement.Criteria;

public class GetReportLegendMappingCriteria : CriteriaBase
{
    public int MenuId { get; set; }

    public string SystemName { get; set; }

    public override string GetSettingKey() => "LegendManagement_GetReportLegendMapping";

    public override bool IsValid() => true;
}