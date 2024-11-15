namespace Utilities.MostVisitedPages
{
    public class MostVisitedPagesService
    {
        //public IEnumerable<MostVisitedPageModel> GetMostVisitedPages()
        //{
        //    if (AppSettings.IsDisableMostvisitedPage)
        //    {
        //        return null;
        //    }

        //    var rawData = this._mostVisitedPagesRepository.FetchMostVisitedPages(new SelectMostVisitedPageCriteria());
        //    var mostvisitedPages = new List<MostVisitedPageModel>();
        //    foreach (var row in rawData)
        //    {
        //        var menu = this._menuService.GetMenuById(Converter.ToInt(row.MenuId));
        //        if (menu != null)
        //        {
        //            mostvisitedPages.Add(new MostVisitedPageModel
        //            {
        //                MenuName = menu.Name,
        //                MenuURL = menu.Url.Trim(),
        //                MenuId = menu.Id,
        //                Count = row.Count,
        //                Position = Converter.ToInt(row.Position),
        //                Title = menu.ParentName + MenuService.UrlSeparator + menu.Name
        //            });
        //        }
        //    }

        //    this.SortMostVisitedPages(ref mostvisitedPages);
        //    return mostvisitedPages;
        //}

        //public void UpdateCount(MostVisitedPageModel mostVisitedPage)
        //{
        //    if (AppSettings.IsDisableMostvisitedPage || mostVisitedPage == null)
        //    {
        //        return;
        //    }

        //    lock (CommonConstants.MostVisitedPageLockObject)
        //    {
        //        List<MostVisitedPage> mostVisitedPages = CacheExtension.Get<List<MostVisitedPageModel>>(CommonConstants.MistVisitedPageCacheKey);
        //        if (mostVisitedPages == null)
        //        {
        //            mostVisitedPages = new List<MostVisitedPageModel>();
        //        }

        //        var mostVisitedPagesCount = mostVisitedPages.Count;
        //        var isExist = false;
        //        for (int i = 0; i < mostVisitedPagesCount; i++)
        //        {
        //            if (mostVisitedPages[i] != null &&
        //                mostVisitedPages[i].MenuId == mostVisitedPage.MenuId)
        //            {
        //                mostVisitedPages[i].Count++;
        //                isExist = true;
        //                break;
        //            }
        //        }

        //        if (!isExist && AppSession.CurrentUser != null)
        //        {
        //            mostVisitedPages.Add(new MostVisitedPage
        //            {
        //                UserId = AppSession.CurrentUser.Id,
        //                MenuId = mostVisitedPage.MenuId
        //            });
        //        }

        //        // Save to cache.
        //        CacheExtension.Set(CommonConstants.MistVisitedPageCacheKey, mostVisitedPages);
        //    }
        //}

        //public void UpdatePosition(ICriteria mostVisitedPage)
        //{
        //    if (AppSettings.IsDisableMostvisitedPage)
        //    {
        //        return;
        //    }

        //    this._mostVisitedPagesRepository.UpdatePosition(mostVisitedPage);
        //}

        //public void Delete(ICriteria mostVisitedPage)
        //{
        //    if (AppSettings.IsDisableMostvisitedPage)
        //    {
        //        return;
        //    }

        //    this._mostVisitedPagesRepository.Delete(mostVisitedPage);
        //}

        //private bool IsInRange(int position, int maxPages)
        //{
        //    return position >= 1 && position <= maxPages;
        //}

        //private void SortMostVisitedPages(ref List<MostVisitedPageModel> mostVisitedPages)
        //{
        //    if (mostVisitedPages == null)
        //    {
        //        return;
        //    }

        //    mostVisitedPages = mostVisitedPages.Take(SelectMostVisitedPageCriteria.MaxPages)
        //                                       .ToList();

        //    var pinSite = mostVisitedPages.Where(mvp =>
        //        this.IsInRange(mvp.Position, SelectMostVisitedPageCriteria.MaxPages)).ToList();
        //    var unpinSite = mostVisitedPages.Where(mvp =>
        //        !this.IsInRange(mvp.Position, SelectMostVisitedPageCriteria.MaxPages)).ToList();
        //    mostVisitedPages = unpinSite;

        //    for (int i = 0; i < pinSite.Count; i++)
        //    {
        //        var mostVisitedPage = pinSite[i];
        //        if (mostVisitedPage.Position <= mostVisitedPages.Count)
        //        {
        //            mostVisitedPages.Insert(mostVisitedPage.Position - 1, mostVisitedPage);
        //        }
        //        else
        //        {
        //            mostVisitedPages.Add(mostVisitedPage);
        //        }
        //    }
        //}
    }
}
