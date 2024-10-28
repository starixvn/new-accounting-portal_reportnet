using Fanex.Common;
using Fanex.Data.Repository;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using LegendManagement.Configurations;
using LegendManagement.Constants;
using LegendManagement.Criteria;
using LegendManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace LegendManagement.Services;

public interface LegendManagementService
{
    LegendResultModel GetReportLegend(GetReportLegendCriteria criteria);

    List<MenuItem> GetMenuList(GetSystemMenuCriteria criteria);

    List<string> MakeInlineImages(List<string> imagesPath, string imageFolder);

    string SaveReportLegend(SaveReportLegendCriteria criteria);

    Task<string> UploadImage(UploadFileCriteria criteria);

    string SaveReportLegendMapping(SaveReportLegendMappingCriteria criteria);

    List<InputedLegend> GetInputedLegendReport(GetInputedLegendReportCriteria criteria);
}

public class LegendManagementServiceImp(IDynamicRepository dynamicRepository,ISiteService siteService, INapSettings napSetting) : LegendManagementService
{
    private const string InlineFileDataSuffix = ".inline";

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

    public List<MenuItem> GetMenuList(GetSystemMenuCriteria criteria)
    {
        var menuList = siteService.GetMenuItemBySiteId(napSetting.SiteId, Constant.UAT, true);
        var result = MappingNapMenuToAccountingMenu(menuList);
        return result;
    }

    public List<string> MakeInlineImages(List<string> imagesPath, string imageFolder)
    {
        if (imagesPath.Count == 0) return [];
        var inlineImages = new List<string>();
        var imageLength = imagesPath.Count;
        for (var i = 0; i < imageLength; i++)
        {
            var imagePath = imagesPath[i];
            if (imagePath.Contains("data:image"))
            {
                continue;
            }

            var base64String = GetInlineData(imagePath, imageFolder);
            inlineImages.Add(base64String);
        }

        return inlineImages;

    }

    public string SaveReportLegend(SaveReportLegendCriteria criteria)
    {
        criteria.LegendId = MappingLegendId(new GetReportLegendCriteria
        {
            MenuId = criteria.MenuId,
            System = WebSettings.System
        });
        dynamicRepository.Execute(criteria);
        return Converter.ToString2(criteria.ErrMsg);
    }

    public async Task<string> UploadImage(UploadFileCriteria criteria)
    {
        var fileName = criteria.FileName[(criteria.FileName.LastIndexOf('\\') + 1)..];
        var savePath = $"{criteria.SavePath}/{fileName}";
        await using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await criteria.File.CopyToAsync(stream);
        }
        var inlineData = MakeInlineImage(criteria.File);
        SaveInlineData(inlineData, savePath + InlineFileDataSuffix);
        return criteria.FileName;
    }

    public string SaveReportLegendMapping(SaveReportLegendMappingCriteria criteria)
    {
        criteria.LegendId = this.MappingLegendId(new GetReportLegendCriteria
        {
            MenuId = criteria.AccMenuId,
            System = WebSettings.System
        });

        if (criteria.LegendId == 0) return "Not exist legend for report!";
        dynamicRepository.Execute(criteria);
        return Converter.ToString2(criteria.Errmsg);

    }

    public List<InputedLegend> GetInputedLegendReport(GetInputedLegendReportCriteria criteria)
    {
        var result = new List<InputedLegend>
        {
            new InputedLegend
            {
                System = "Accounting",
                ReportId = 101,
                ReportName = "Daily Win Loss 2h",
                Content = "abc"
            },
            new InputedLegend
            {
                System = "NAP",
                ReportId = 101,
                ReportName = "Daily Win Loss 2h",
                Content = "abc"
            },
            new InputedLegend
            {
                System = "NAP",
                ReportId = 103,
                ReportName = "Bet Count",
                Content = "Bet Count"
            }
        };

        return result;
    }

    private static string MakeInlineImage(IFormFile file)
    {
        using var br = new BinaryReader(file.OpenReadStream());
        var buffer = new byte[16 * 5000];
        using var ms = new MemoryStream();
        int read;
        while ((read = br.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        var content = ms.ToArray();
        var base64String = Convert.ToBase64String(content, 0, content.Length);

        return base64String;
    }

    private static void SaveInlineData(string inlineData, string filePath)
    {
        using var file = new StreamWriter(filePath);
        file.WriteLine(inlineData);
        file.Flush();
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

    private List<MenuItem> MappingNapMenuToAccountingMenu(IEnumerable<Fanex.NAP.Client.Models.MenuItem> rawData, int? parentId = null)
    {
        var menuList = new List<MenuItem>();
        if (rawData == null)
        {
            return menuList;
        }

        if (rawData.Any())
        {
            var rows = rawData.Where(menuItem =>
                (parentId == null && menuItem.ParentId == 0) ||
                (menuItem.ParentId != 0 && menuItem.ParentId == parentId));

            if (rows != null && rows.Any())
            {
                menuList = new List<MenuItem>();
                foreach (var row in rows)
                {
                    var menuItem = new MenuItem
                    {
                        Id = Converter.ToInt(row.Id),
                        Name = Converter.ToString2(row.Name),
                        Href = Converter.ToString2(row.Url),
                        System = WebSettings.System,
                        ParentId = Converter.ToInt(parentId),
                        FunctionName = Converter.ToString2(row.FunctionName),
                        IsHide = !Converter.ToBool(row.IsActive),
                        Order = Converter.ToInt(row.DisplayOrder),
                        SubMenu = this.MappingNapMenuToAccountingMenu(rawData, row.Id)
                    };

                    menuItem.SubMenu = this.MappingNapMenuToAccountingMenu(rawData, menuItem.Id);
                    menuList.Add(menuItem);
                }
            }
        }

        return menuList;
    }

    private static string GetInlineData(string imagePath, string imageFolder)
    {
        var result = string.Empty;
        if (string.IsNullOrEmpty(imagePath)) return result;
        var filePath = string.Concat(imageFolder, "/", imagePath.AsSpan(imagePath.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1)) + InlineFileDataSuffix;
        return !File.Exists(filePath) ? result : File.ReadAllText(filePath);
    }
}