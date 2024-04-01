import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { HttpService } from 'nap-components/http';
import { NavbarService, UserMenu, Menu, Navbar } from 'nap-components/navbar';

@Injectable()
export class CustomNavbarService extends NavbarService {
    url = 'state-api/application/header';
    userMenus: UserMenu[] = [];
    legacyPath: string;
    constructor(
        private customHttpService: HttpService
    ) {
        super(customHttpService);
    }

    query(): Observable<Navbar> {
        const url = 'state-api/application/header';
        return this.customHttpService.post<Navbar>(url).map(data => {
            const legacyNavbar = data as any;
            this.legacyPath = legacyNavbar.legacyPath as string;
            this.userMenus = [
                {
                    name: 'Change Password',
                    link: this.legacyPath + '/Auth/ChangePassword'
                },
                {
                    name: 'Sign Out',
                    link: this.legacyPath + '/Auth/LogOut'
                }
            ];

            return data;
        });
    }

    compileMenus(menus: Menu[]): Menu[] {
        menus.forEach(menu => {
            menu.hasChildren = menu.children !== null && menu.children.length > 0;
            menu.link = this.resolveUniveralUrl(menu.link);
            if (menu.hasChildren) {
                menu.children.forEach(childMenu => {
                    childMenu.hasChildren = false;
                    childMenu.link = this.resolveUniveralUrl(childMenu.link);
                });
            }
        });

        return menus;
    }

    private resolveUniveralUrl(url: string): string {
        return this.isUniversalUrl(url)
            ? url
            : this.legacyPath + url;
    }
    private isUniversalUrl(url: string): boolean {
        return url.indexOf('/UniversalReports') === 0;
    }
}
