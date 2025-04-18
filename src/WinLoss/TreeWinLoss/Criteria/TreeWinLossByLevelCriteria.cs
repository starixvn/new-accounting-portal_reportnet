namespace TreeWinLoss.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class TreeWinLossByLevelCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsNormalMode { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Currency { get; set; }

    [SpParam(SqlParamName = "srecommend")]
    public int SRecommend { get; set; }

    [SpParam(SqlParamName = "mrecommend")]
    public int MRecommend { get; set; }

    public int Recommend { get; set; }

    public int CustId { get; set; }

    [SpParam(Ignored = true)]
    public string Name { get; set; }

    [SpParam(Ignored = true)]
    public int RoleId { get; set; }

    [SpParam(Ignored = true)]
    public string ProductSelection { get; set; }

    public string Products { get; set; }

    public string SportType { get; set; }

    public override bool IsValid()
    {
        return true;
    }

        
    public override string GetSettingKey()
    {
        var settingKey = RoleId switch
        {
            2 => "WinLoss_TreeWinLoss_GetDataByAgent",
            3 => "WinLoss_TreeWinLoss_GetDataByMaster",
            4 => "WinLoss_TreeWinLoss_GetDataBySuper",
            5 => "WinLoss_TreeWinLoss_GetDataByCom",
            _ => "WinLoss_TreeWinLoss_GetDataByAgent"
        };

        return settingKey;
    }
}