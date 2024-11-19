using Fanex.NAP.Client.Helpers.Web;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Nap_Reports.Sites.ViewComponents;

public class MostVisitedPagesViewComponent(INapSession napSession) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = napSession.MenuItemList;
        return await Task.FromResult(View(model));
    }
}