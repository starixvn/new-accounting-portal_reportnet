using Fanex.Data.Repository;

namespace LegendManagement.Criteria;

public class GetMenuLegendCriteria : CriteriaBase
{
    public int MenuId { get; set; }

    public int MenuLegendId { get; set; }

    public string ConfigPath { get; set; }

    public string FunctionName { get; set; }

    public override string GetSettingKey() => "SelMenuLegend";

    public override bool IsValid() => true;
}