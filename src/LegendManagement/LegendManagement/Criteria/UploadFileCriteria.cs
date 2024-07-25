using Fanex.Data.Repository;
using Microsoft.AspNetCore.Http;

namespace LegendManagement.Criteria;

public class UploadFileCriteria : CriteriaBase
{
    public IFormFile File { get; set; }

    public string FileName { get; set; }

    public string SavePath { get; set; }

    public override string GetSettingKey() => string.Empty;

    public override bool IsValid() => true;
}