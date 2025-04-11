using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using NPOI.SS.Formula.Functions;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Model;

namespace SoccerPerformanceDetail.Services;

public interface PermissionService
{
    Task<BettypeViewPermission> GetBettypePermission();

    bool IsVirtualPerformancePermission();

    bool IsRealPerformancePermission();

    bool IsSoccerNoneLivePermission();

    bool IsSoccerLivePermission();

    bool HasNotPermissionToAccessPage();

    Dictionary<Market, string> GetMarketsPermission();
}

public class PermissionServiceImp(IUserService userService, INapSession napSession, INapSettings napSettings) : PermissionService
{
    public async Task<BettypeViewPermission> GetBettypePermission()
    {
        var permissionTasks = new[]
        {
            userService.IsHavingPermissionAsync(napSession.UserId, napSettings.SiteId, 
                FunctionKeyConstants.PerformanceDetailPHBettypes, Constant.Policy.Read),
            userService.IsHavingPermissionAsync(napSession.UserId, napSettings.SiteId, 
                FunctionKeyConstants.PerformanceDetailMajorBettypes, Constant.Policy.Read)
        };

        await Task.WhenAll(permissionTasks);
        
        var hasPH = permissionTasks[0].Result;
        var hasMajor = permissionTasks[1].Result;
        
        return (hasPH, hasMajor) switch
        {
            (true, true) => BettypeViewPermission.All,
            (true, false) => BettypeViewPermission.PHAIO_Only,
            (false, true) => BettypeViewPermission.Major_Only,
            _ => BettypeViewPermission.None
        };
    }

    public bool IsVirtualPerformancePermission()
    {
        return userService.GetPermission(napSession.UserId, napSettings.SiteId, FunctionKeyConstants.VirtualPerformance) >=
               (int)Constant.Policy.Read;
    }

    public bool IsRealPerformancePermission()
    {
        return userService.GetPermission(napSession.UserId, napSettings.SiteId, FunctionKeyConstants.RealPerformance) >=
               (int)Constant.Policy.Read;
    }

    public bool IsSoccerNoneLivePermission()
    {
        return userService.GetPermission(napSession.UserId, napSettings.SiteId, FunctionKeyConstants.SoccerNon_Live) >=
               (int)Constant.Policy.Read;
    }

    public bool IsSoccerLivePermission()
    {
        return userService.GetPermission(napSession.UserId, napSettings.SiteId, FunctionKeyConstants.SoccerLive) >= (int)Constant.Policy.Read;
    }

    public bool HasNotPermissionToAccessPage()
    {
        return (!IsSoccerLivePermission() && !IsSoccerNoneLivePermission()) ||
               (!IsRealPerformancePermission() && !IsVirtualPerformancePermission());
    }

    public Dictionary<Market, string> GetMarketsPermission()
    {
        var markets = new Dictionary<Market, string>();

        if (IsSoccerLivePermission() && IsSoccerNoneLivePermission())
        {
            markets.Add(Market.All, "All");
            markets.Add(Market.Live, "Live");
            markets.Add(Market.NonLive, "Non-Live");
        }
        else
        {
            if (IsSoccerLivePermission())
            {
                markets.Add(Market.Live, "Live");
            }

            if (IsSoccerNoneLivePermission())
            {
                markets.Add(Market.NonLive, "Non-Live");
            }
        }

        return markets;
    }
}