namespace Reports.Sites.ViewComponents;

using Microsoft.AspNetCore.Mvc;

public class ScoreInformationViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}