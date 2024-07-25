using Fanex.Data;
using Fanex.Data.Repository;
using LegendManagement.Configurations;

namespace LegendManagement.Criteria;

public class SaveReportLegendCriteria : CriteriaBase
{
    public SaveReportLegendCriteria()
    {
        this.System = WebSettings.System;
    }

    public int MenuId { get; set; }

    public string Legend { get; set; }

    public int LegendId { get; set; }

    public string System { get; set; }

    [SpParam(Ignored = true)]
    public bool IsNote { get; set; }

    [SpParam(Ignored = true)]
    public bool IsReviewed { get; set; }

    [SpParam(Ignored = true)]
    public string ConfigPath { get; set; }

    public override string GetSettingKey() => "LegendManagement_SaveReportLegend";

    public override bool IsValid() => true;
}