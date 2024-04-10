using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Utilities.Header
{
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly INapSession napSession;

        public HeaderController(INapSession napSession)
        {
            this.napSession = napSession;
        }

        [Route("api/application/header")]
        public Header Index()
        {
            var sessionMenus = napSession.MenuItemList ?? new List<MenuItem>();
            var menus = GetMenus(sessionMenus);
            return new Header
            {
                Username = napSession.UserName,
                Menus = menus
            };
        }

        [Route("state-api/application/header")]
        [HttpPost]
        public IActionResult Header()
        {

            var sessionMenus = napSession.MenuItemList ?? new List<MenuItem>();
            var menus = GetMenus(sessionMenus);
            var header = new Header
            {
                Username = napSession.UserName,
                Menus = menus
            };

            return Ok(header);
        }

        private List<Menu> GetMenus(IList<MenuItem> menus)
        {
            return menus.Where(m => m.ParentId == 0)
                               .OrderBy(m => m.DisplayOrder)
                               .Select(m => new Menu
                               {
                                   Id = m.Id,
                                   Name = m.Name,
                                   Link = m.Url,
                                   Children = GetChildrenMenus(menus, m.Id)
                               })
                               .ToList();
        }

        private List<Menu> GetChildrenMenus(IEnumerable<MenuItem> menus, int parentMenuId)
        {
            return menus.Where(x => x.ParentId == parentMenuId)
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => new Menu
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Link = m.Url
                        })
                        .ToList();
        }
    }
}
