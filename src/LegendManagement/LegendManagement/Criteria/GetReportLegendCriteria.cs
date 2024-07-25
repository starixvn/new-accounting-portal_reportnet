using Fanex.Data;
using Fanex.Data.Repository;
using LegendManagement.Configurations;

namespace LegendManagement.Criteria;

public class GetReportLegendCriteria : CriteriaBase
{
    public GetReportLegendCriteria()
    {
        this.MenuId = WebSettings.DefaultReportLegendTemplate;
        this.LegendId = WebSettings.DefaultReportLegendTemplate;
        this.System = WebSettings.System;
        this.IsNote = false;
    }

    public int MenuId { get; set; }

    public string System { get; set; }

    [SpParam(Ignored = true)]
    public bool IsNote { get; set; }

    [SpParam(Ignored = true)]
    public int LegendId { get; set; }

    [SpParam(Ignored = true)]
    public string ConfigPath { get; set; }

    [SpParam(Ignored = true)]
    public string FunctionName { get; set; }

    [SpParam(Ignored = true)]
    public string CallBackFunction { get; set; }

    public override string GetSettingKey() => "LegendManagement_SelReportLegend";

    public override bool IsValid() => true;
}