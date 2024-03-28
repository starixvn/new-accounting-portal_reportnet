using Fanex.NAP.Client.Helpers.Web;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RiskControl.Sites.ViewComponents;

public class MenuViewComponent(INapSession napSession) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = napSession.MenuItemList;
        return await Task.FromResult(View(model));
    }
}