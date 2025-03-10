namespace Utilities.MostVisitedPages;

using Fanex.NAP.Client;
using Fanex.NAP.Client.Models;

public interface MenuService
{
    string GetMenuPathFromRequestFilePath(string filePath, string applicationPath);

    MenuItem GetMenuById(int menuId);

    MenuItem GetMenuByURL(string menuURL);

    MenuItem GetMenuByURL(string menuURL, IList<MenuItem> menuList);

    string GetMenuNameByURL(string menuURL);
}

public class MenuServiceImp(INapSession napSession) : MenuService
{
    public const char UrlSeparator = '/';

    public string GetMenuPathFromRequestFilePath(
        string filePath,
        string applicationPath)
    {
        filePath = filePath.Trim();
        applicationPath = applicationPath.Trim();
        var menuPath = filePath;
        if (!string.IsNullOrEmpty(applicationPath) &&
            menuPath.StartsWith(applicationPath, StringComparison.CurrentCultureIgnoreCase) &&
            napSession.MenuItemList != null)
        {
            menuPath = menuPath.Remove(0, applicationPath.Length);
        }

        if (!menuPath.StartsWith(UrlSeparator.ToString()))
        {
            menuPath = UrlSeparator + menuPath;
        }

        return menuPath;
    }

    public MenuItem GetMenuById(int menuId)
    {
        var menu = new MenuItem();

        if (napSession.MenuItemList != null)
        {
            menu = napSession.MenuItemList.FirstOrDefault(m => m.Id == menuId);

        }

        return menu;
    }

    public MenuItem GetMenuByURL(string menuURL)
    {
        var menu = new MenuItem();

        if (!string.IsNullOrEmpty(menuURL) &&
            !string.Equals(menuURL, UrlSeparator.ToString()) &&
            napSession.MenuItemList != null)
        {
            menu = napSession.MenuItemList.FirstOrDefault(m => m.Url.ToLower().StartsWith(menuURL.ToLower()));
        }

        return menu;
    }

    public MenuItem GetMenuByURL(string menuURL, IList<MenuItem> menuList)
    {
        var menu = new MenuItem();

        if (!string.IsNullOrEmpty(menuURL) &&
            !string.Equals(menuURL, UrlSeparator.ToString()) &&
            napSession.MenuItemList != null)
        {
            menu = menuList.FirstOrDefault(m => m.Url.StartsWith(menuURL));
        }
           
        return menu;
    }

    public string GetMenuNameByURL(string menuURL)
    {
        var menu = new MenuItem();

        if (!string.IsNullOrEmpty(menuURL) &&
            !string.Equals(menuURL, UrlSeparator.ToString()) &&
            napSession.MenuItemList != null)
        {
            menu = napSession.MenuItemList.FirstOrDefault(m => m.Url.StartsWith(menuURL));
        }

        return menu?.Name;
    }
}