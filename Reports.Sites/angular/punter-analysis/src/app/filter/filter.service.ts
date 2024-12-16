import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';
import { TreeviewItem, TreeItem } from 'ngx-treeview';
import { RoleId } from 'nap-components/model';
import { HttpService } from 'nap-components/http';
import { SportsbookSportTypeBetType } from './filter.model.sportsbook-sporttype-bettype';
export interface DataWarehouseDateRange {
    MinDate: string;
    MaxDate: string;
}

interface ProductSportTypeBetType {
    ProductName: string;
    SportGroup: string;
    SportName: string;
    BetTypeName: string;
    Value: number;
    RenderType: number;
    BetTypeGroup: string;
}

interface SportTypeLeague {
    LeagueId: string;
    LeagueName: string;
}

enum RenderType {
    Product_SportGroup_Sport_BetType = 1,
    Product_Sport_BetType = 2,
    Product_BetType = 3,
    Product_Sport_BetTypeGroup_BetType = 4
}

interface LeagueGroup {
    SportType: number;
    Sport: string;
    LeagueGroupId: number;
    LeagueGroupName: string;
}

@Injectable()
export class FilterService {
    private url = 'api/punter-analysis/filter';

    constructor(
        private httpService: HttpService
    ) { }

    queryDataWarehouseDateRange(): Observable<DataWarehouseDateRange> {
        const url = `${this.url}/available-date-range`;
        return this.httpService.post<DataWarehouseDateRange>(url);
    }

    queryUserNameSuggestion(userName: string, roleId: RoleId): Observable<string[]> {
        const url = `${this.url}/username-suggestion`;
        return this.httpService.post<string[]>(url, {
            userName: userName,
            roleId: roleId
        });
    }

    queryProductTreeviewItems(): Observable<TreeviewItem[]> {
        const url = `${this.url}/product-sport-bet-type`;
        return this.httpService.post<ProductSportTypeBetType[]>(url).map(list => {
            return this.buildProductTreeviewItems(list);
        });
    }

    checkDatabaseAvailable(): Observable<boolean> {
        const url = `${this.url}/available-database`;
        return this.httpService.post<boolean>(url);
    }

    querySportbooksSportTypeBetType(): Observable<SportsbookSportTypeBetType[]> {
        const url = `${this.url}/sportsbook-sport-bet-type`;
        return this.httpService.post<SportsbookSportTypeBetType[]>(url);
    }

    querySportTypeLeague(sportId: number, searchText: string, searchExactly: boolean): Observable<SportTypeLeague[]> {
        const url = `${this.url}/sport-type-league`;
        return this.httpService.post<SportTypeLeague[]>(url, {
            sportId: sportId,
            searchText: searchText,
            searchExactly: searchExactly
        });
    }

    queryLeagueGroupTreeviewItems(): Observable<TreeviewItem[]> {
        const url = `${this.url}/league-group`;
        return this.httpService.post<LeagueGroup[]>(url).map(list => {
            return this.buildLeagueGroupTreeviewItems(list);
        });
    }

    private buildProductTreeviewItems(list: ProductSportTypeBetType[]): TreeviewItem[] {
        const productItems: TreeviewItem[] = [];
        const productNames = _.uniq(list.map(x => x.ProductName));
        productNames.forEach(productName => {
            const productItem = new TreeviewItem({
                text: productName,
                value: productName,
                collapsed: true
            });
            productItems.push(productItem);
            const listByProductName = list.filter(x => x.ProductName === productName);
            listByProductName.forEach(item => {
                let currentTreeviewItem = productItem;
                const treeItems = this.getProductTreeItems(item);
                treeItems.forEach(treeItem => {
                    let childItem: TreeviewItem;
                    if (currentTreeviewItem.children) {
                        for (let i = 0; i < currentTreeviewItem.children.length; i++) {
                            const child = currentTreeviewItem.children[i];
                            if (child.text === treeItem.text && child.value === treeItem.value) {
                                childItem = child;
                                break;
                            }
                        }
                    }
                    if (!childItem) {
                        childItem = new TreeviewItem(treeItem);
                        if (currentTreeviewItem.children) {
                            currentTreeviewItem.children.push(childItem);
                        } else {
                            currentTreeviewItem.children = [childItem];
                        }
                    }
                    currentTreeviewItem = childItem;
                });
            });
        });

        return productItems;
    }

    private getProductTreeItems(item: ProductSportTypeBetType): TreeItem[] {
        let renderTypeNodes: string[] = [];
        switch (item.RenderType) {
            case RenderType.Product_SportGroup_Sport_BetType:
                renderTypeNodes = [item.SportGroup, item.SportName];
                break;
            case RenderType.Product_Sport_BetType:
                renderTypeNodes = [item.SportName];
                break;
            case RenderType.Product_Sport_BetTypeGroup_BetType:
                renderTypeNodes = [item.SportName, item.BetTypeGroup];
                break;
        }

        const treeItems = renderTypeNodes.map(node => <TreeItem>{
            text: node,
            value: item.RenderType,
            collapsed: true
        });
        treeItems.push({
            text: item.BetTypeName,
            value: item.Value,
            collapsed: true
        });
        return treeItems;
    }

    private buildLeagueGroupTreeviewItems(list: LeagueGroup[]): TreeviewItem[] {
        const leagueGroupItems: TreeviewItem[] = [];
        const sportNames = _.uniq(list.map(x => x.Sport));
        sportNames.forEach(sportName => {
            const sportItem = new TreeviewItem({
                text: sportName,
                value: sportName,
                collapsed: true
            });

            leagueGroupItems.push(sportItem);
            const listBySportName = list.filter(x => x.Sport === sportName);
            listBySportName.forEach(item => {
                const treeItem = new TreeviewItem({ text: item.LeagueGroupName, value: item.LeagueGroupId });

                if (_.isNil(sportItem.children)) {
                    sportItem.children = [treeItem];
                } else {
                    sportItem.children.push(treeItem);
                }
            });
        });

        return leagueGroupItems;
    }
}
