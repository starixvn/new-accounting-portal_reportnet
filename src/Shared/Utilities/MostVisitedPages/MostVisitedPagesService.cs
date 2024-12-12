using Fanex.Caching;
using Fanex.Data.Repository;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client;
using Utilities.Configurations;

namespace Utilities.MostVisitedPages
{
    public interface MostVisitedPagesService
    {
        Task<IEnumerable<MostVisitedPageModel>> GetMostVisitedPages();

        void UpdateCount(MostVisitedPageModel mostVisitedPage);

        Task<int> UpdatePosition(INonQueryCommand mostVisitedPage);

        Task<int> Delete(INonQueryCommand mostVisitedPage);
    }

    public class MostVisitedPagesServiceImp(
        ConfigurationService configurationService, 
        IDynamicRepository repository,
        INapSettings napSettings,
        INapSession napSession,
        MenuService menuService,
        ICacheService cacheService) : MostVisitedPagesService
    {
        public const string MistVisitedPageCacheKey = "Fanex.AccountingReports.Core.Contants.AppSession.IntervalGetMostVisitedPage";

        public const char UrlSeparator = '/';

        public static readonly object MostVisitedPageLockObject = new();

        public async Task<IEnumerable<MostVisitedPageModel>> GetMostVisitedPages()
        {
            if (configurationService.IsDisableMostvisitedPage)
            {
                return null;
            }

            var criteria = new SelectMostVisitedPageCriteria
            {
                Environment = napSettings.Environment,
                UserId = napSession.UserId,
                SiteId = napSettings.SiteId
            };

            var rawData =  await repository.FetchAsync<MostVisitedPageModel>(criteria);

            var mostvisitedPages = new List<MostVisitedPageModel>();

            foreach (var row in rawData)
            {
                var menu = menuService.GetMenuById(row.MenuId);

                if (menu != null)
                {
                    mostvisitedPages.Add(new MostVisitedPageModel
                    {
                        MenuName = menu.Name,
                        MenuURL = menu.Url.Trim(),
                        MenuId = menu.Id,
                        Count = row.Count,
                        Position = row.Position,
                        Title = menu.ParentName + UrlSeparator + menu.Name
                    });
                }
            }

            SortMostVisitedPages(ref mostvisitedPages);
            return mostvisitedPages;
        }

        public void UpdateCount(MostVisitedPageModel mostVisitedPage)
        {
            if (configurationService.IsDisableMostvisitedPage || mostVisitedPage == null)
            {
                return;
            }

            lock (MostVisitedPageLockObject)
            {
                var mostVisitedPages = cacheService.Get<List<MostVisitedPageModel>>(MistVisitedPageCacheKey);
                if (mostVisitedPages == null)
                {
                    mostVisitedPages = [];
                }

                var mostVisitedPagesCount = mostVisitedPages.Count;
                var isExist = false;
                for (var i = 0; i < mostVisitedPagesCount; i++)
                {
                    if (mostVisitedPages[i] != null &&
                        mostVisitedPages[i].MenuId == mostVisitedPage.MenuId)
                    {
                        mostVisitedPages[i].Count++;
                        isExist = true;
                        break;
                    }
                }

                if (!isExist && napSession.UserId != 0)
                {
                    mostVisitedPages.Add(new MostVisitedPageModel
                    {
                        UserId = napSession.UserId,
                        MenuId = mostVisitedPage.MenuId
                    });
                }

                // Save to cache.
                cacheService.Set(MistVisitedPageCacheKey, mostVisitedPages);
            }
        }

        public async Task<int> UpdatePosition(INonQueryCommand mostVisitedPage)
        {
            if (configurationService.IsDisableMostvisitedPage)
            {
                return -1;
            }

            return await repository.ExecuteAsync(mostVisitedPage);
        }

        public async Task<int> Delete(INonQueryCommand mostVisitedPage)
        {
            if (configurationService.IsDisableMostvisitedPage)
            {
                return -1;
            }

            return await repository.ExecuteAsync(mostVisitedPage);
        }

        private bool IsInRange(int position, int maxPages)
        {
            return position >= 1 && position <= maxPages;
        }

        private void SortMostVisitedPages(ref List<MostVisitedPageModel> mostVisitedPages)
        {
            if (mostVisitedPages == null)
            {
                return;
            }

            mostVisitedPages = mostVisitedPages.Take(SelectMostVisitedPageCriteria.MaxPages)
                                               .ToList();

            var pinSite = mostVisitedPages.Where(mvp =>
                IsInRange(mvp.Position, SelectMostVisitedPageCriteria.MaxPages)).ToList();
            var unpinSite = mostVisitedPages.Where(mvp =>
                !IsInRange(mvp.Position, SelectMostVisitedPageCriteria.MaxPages)).ToList();
            mostVisitedPages = unpinSite;

            for (var i = 0; i < pinSite.Count; i++)
            {
                var mostVisitedPage = pinSite[i];
                if (mostVisitedPage.Position <= mostVisitedPages.Count)
                {
                    mostVisitedPages.Insert(mostVisitedPage.Position - 1, mostVisitedPage);
                }
                else
                {
                    mostVisitedPages.Add(mostVisitedPage);
                }
            }
        }
    }
}
