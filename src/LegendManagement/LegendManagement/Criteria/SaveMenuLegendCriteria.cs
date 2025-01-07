using Fanex.Data.Repository;

namespace LegendManagement.Criteria;

public class SaveMenuLegendCriteria : CriteriaBase
{
    public int MenuId { get; set; }

    public string MenuLegend { get; set; }

    public int MenuLegendId { get; set; }

    public string ConfigPath { get; set; }

    public override string GetSettingKey() => "SaveMenuLegend";

    public override bool IsValid() => true;
}