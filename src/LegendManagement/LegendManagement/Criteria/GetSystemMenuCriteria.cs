namespace LegendManagement.Criteria;

using Configurations;
using Fanex.Data;
using Fanex.Data.Repository;

public class GetSystemMenuCriteria : CriteriaBase
{
    [SpParam(SqlParamName = "system")]
    public string System { get; set; } = WebSettings.System;

    public override string GetSettingKey() => "LegendManagement_SelMenuListBySystem";

    public override bool IsValid() => true;
}