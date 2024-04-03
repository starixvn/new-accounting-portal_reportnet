import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { HttpService } from 'nap-components/http';
import { NavbarService, UserMenu, Menu, Navbar } from 'nap-components/navbar';

@Injectable()
export class CustomNavbarService extends NavbarService {
    url = 'state-api/application/header';
    menu: Menu[] = [];
    userMenus: UserMenu[] = [];
    constructor(
        private customHttpService: HttpService
    ) {
        super(customHttpService);
    }

    query(): Observable<Navbar> {
        const url = 'state-api/application/header';


        return this.customHttpService.post<any>(url).map(data => {
            this.userMenus = [
                {
                    name: 'Change Password',
                    link: 'Auth/ChangePassword'
                },
                {
                    name: 'Sign Out',
                    link: 'Auth/LogOut'
                }
            ];

            var result: Navbar =
            {
              menus: this.bindingMenuData(data.Menus),
              username: data.Username
            };

            return result;
        });
    }

    // compileMenus(menus: Menu[]): Menu[] {
    //   return menus;
    // }

    private bindingMenuData(data: any): Menu[]
    {
      let menus: Menu[] = [];
      data.forEach(menu => {
        let hasChildrend = menu.Children !== null && menu.Children.length > 0;
        let children: Menu[] = [];

        if(hasChildrend) {
          menu.Children.forEach(childMenu => {
            children.push({
                id: childMenu.Id,
                name: childMenu.Name,
                link:childMenu.Link,
                children: []
            } as Menu);
          });
        }
        menus.push({
          id: menu.Id,
          name: menu.Name,
          link: menu.Link,
          children: children
        } as Menu);
      });

      return menus;
    }
}
