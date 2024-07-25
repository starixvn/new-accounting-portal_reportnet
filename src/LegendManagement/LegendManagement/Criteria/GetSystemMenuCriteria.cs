using Fanex.Data;
using Fanex.Data.Repository;
using LegendManagement.Configurations;

namespace LegendManagement.Criteria;

public class GetSystemMenuCriteria : CriteriaBase
{
    public GetSystemMenuCriteria() => this.System = WebSettings.System;

    [SpParam(SqlParamName = "system")]
    public string System { get; set; }

    public override string GetSettingKey() => "LegendManagement_SelMenuListBySystem";

    public override bool IsValid() => true;
}