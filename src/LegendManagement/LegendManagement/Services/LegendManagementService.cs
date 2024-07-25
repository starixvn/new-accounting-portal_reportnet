using Fanex.Data.Repository;
using LegendManagement.Configurations;
using LegendManagement.Constants;
using LegendManagement.Criteria;
using LegendManagement.Models;

namespace LegendManagement.Services;

public interface LegendManagementService
{
    LegendResultModel GetReportLegend(GetReportLegendCriteria criteria);
}

public class LegendManagementServiceImp(IDynamicRepository dynamicRepository) : LegendManagementService
{
    public LegendResultModel GetReportLegend(GetReportLegendCriteria criteria)
    {
        var rawData = dynamicRepository.Fetch<RawLegendResultModel>(criteria).FirstOrDefault();
        if (rawData != null)
        {
            var reportLegendContent = rawData.Legend;
            return new LegendResultModel
            {
                Content = reportLegendContent,
                IsDefault = false
            };
        }
        criteria.LegendId = this.MappingLegendId(new GetReportLegendCriteria());
        rawData = dynamicRepository.Fetch<RawLegendResultModel>(criteria).FirstOrDefault();

        return new LegendResultModel
        {
            Content = rawData != null ? rawData.Legend : LabelConstants.SampleTemplate,
            IsDefault = true
        };
    }

    private int MappingLegendId(GetReportLegendCriteria criteria)
    {
        var legend = dynamicRepository.Get<RawLegendResultModel>(new GetReportLegendCriteria
        {
            MenuId = criteria.MenuId,
            System = WebSettings.System
        });

        return legend?.LegendID ?? 0;
    }
}