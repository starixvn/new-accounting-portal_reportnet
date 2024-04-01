import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { NumberFormatHelper, NumberFormatType } from 'nap-components/number-format';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { AssociatedAccountGridModel } from '../associated-account-grid/associated-account-grid.model';
import { SportTypeGridModel } from '../sport-type-grid/sport-type-grid.model';
import { SummaryData } from './summary-grid.model';

@Injectable()
export class SummaryGridService {
    private url = 'api/punter-analysis/summary';

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    createBetCountGridModel(custId: number, custName: string): BetCountGridModel {
        return {
            fromGridType: GridType.UserName,
            custId: custId,
            custName: custName
        };
    }

    createSportTypeGridModel(custId: number, custName: string): SportTypeGridModel {
        return {
            custId: custId,
            custName: custName
        };
    }

    createAssociatedAccountGridModel(custId: number, custName: string): AssociatedAccountGridModel {
        return {
            custId: custId,
            custName: custName
        };
    }

    query(): Observable<SummaryData> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<SummaryData>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        const queryModel = this.createQueryModel();
        queryModel.title = `Punter Analysis ${filterModelWrapper.dateFromToTitle}`;
        const filterLines = [];
        if (filterModel.searchBy === SearchBy.User) {
            filterLines.push(`Depth: ${filterModelWrapper.depthTitle} - Account name: ${filterModelWrapper.userNameTitle}`);
        } else if (filterModel.searchBy === SearchBy.League) {
            filterLines.push('User Name: All');
        }
        const topRow = filterModelWrapper.userLeagueModel.topRow;
        const orderByTitle = filterModelWrapper.userLeagueWrapper.orderByTitle;
        const sortByTitle = filterModelWrapper.userLeagueWrapper.sortByTitle;
        const topRowString = NumberFormatHelper.format(topRow, NumberFormatType.Integer);
        filterLines.push(`Top rows: ${topRowString} - Order by: ${orderByTitle}, ${sortByTitle}`);
        filterLines.push(`Sport Type: ${filterModelWrapper.selectedSportTypes}`);
        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
        }
        queryModel.filter = filterLines.join('\n');
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Top_Punter_Summary_${filterModelWrapper.searchByFileName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }

    private createQueryModel(): any {
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        const userLeagueModel = filterModelWrapper.userLeagueModel;
        return {
            topRow: userLeagueModel.topRow,
            orderBy: userLeagueModel.orderBy,
            sortBy: userLeagueModel.sortBy,
            periodType: userLeagueModel.periodType,
            fromDate: filterModelWrapper.fromDateQueryString,
            toDate: filterModelWrapper.toDateQueryString,
            depth: filterModel.user ? filterModel.user.depth.role : 0,
            userName: filterModel.user ? filterModel.user.userName : '',
            listCombinationId: filterModelWrapper.userLeagueWrapper.listCombinationId,
            leagueId: filterModelWrapper.leagueId,
            listLeagueGroupId: filterModelWrapper.userLeagueWrapper.listLeagueGroupCombinationId
        };
    }
}
