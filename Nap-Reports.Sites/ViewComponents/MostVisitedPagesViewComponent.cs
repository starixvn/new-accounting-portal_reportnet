using Microsoft.AspNetCore.Mvc;

namespace Nap_Reports.Sites.ViewComponents;

using Fanex.NAP.Client;

public class MostVisitedPagesViewComponent(INapSession napSession) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = napSession.MenuItemList;
        return await Task.FromResult(View(model));
    }
}