namespace LegendManagement.Criteria;

using Configurations;
using Fanex.Data;
using Fanex.Data.Repository;

public class GetReportLegendCriteria : CriteriaBase
{
    public int MenuId { get; set; } = WebSettings.DefaultReportLegendTemplate;

    public string System { get; set; } = WebSettings.System;

    [SpParam(Ignored = true)]
    public bool IsNote { get; set; } = false;

    [SpParam(Ignored = true)]
    public int LegendId { get; set; } = WebSettings.DefaultReportLegendTemplate;

    [SpParam(Ignored = true)]
    public string ConfigPath { get; set; }

    [SpParam(Ignored = true)]
    public string FunctionName { get; set; }

    [SpParam(Ignored = true)]
    public string CallBackFunction { get; set; }

    public override string GetSettingKey() => "LegendManagement_SelReportLegend";

    public override bool IsValid() => true;
}