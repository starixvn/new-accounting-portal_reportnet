using Fanex.Data;
using Fanex.Data.Repository;
using LegendManagement.Configurations;

namespace LegendManagement.Criteria;

public class GetSystemMenuCriteria : CriteriaBase
{
    [SpParam(SqlParamName = "system")]
    public string System { get; set; } = WebSettings.System;

    public override string GetSettingKey() => "LegendManagement_SelMenuListBySystem";

    public override bool IsValid() => true;
}