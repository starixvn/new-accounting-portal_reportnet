using Microsoft.AspNetCore.Mvc;

namespace Reports.Sites.ViewComponents;

public class ScoreInformationViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}