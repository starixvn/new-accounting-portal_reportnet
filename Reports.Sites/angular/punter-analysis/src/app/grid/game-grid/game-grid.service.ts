import { GameGridModel } from './game-grid.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { BetTypeGroupGridModel } from '../bet-type-group-grid/bet-type-group-grid.model';

@Injectable()
export class GameGridService {
    private url = 'api/punter-analysis/game';
    model: GameGridModel;

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    createBetTypeGroupGridModel(leagueGroupId: number, leagueGroupName: string): BetTypeGroupGridModel {
        return _.extend(this.model, {
            leagueGroupId: leagueGroupId,
            leagueGroupName: leagueGroupName
        });
    }

    createBetCountGridModel(leagueGroupId: number, leagueGroupName: string): BetCountGridModel {
        return {
            fromGridType: GridType.Game,
            custId: this.model.custId,
            custName: this.model.custName,
            sportId: this.model.sportId,
            sportName: this.model.sportName,
            leagueGroupId: leagueGroupId,
            leagueGroupName: leagueGroupName
        };
    }

    query(): Observable<any> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<any>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        const queryModel = this.createQueryModel();
        queryModel.title = `Punter Analysis ${filterModelWrapper.dateFromToTitle}`;
        const filterLines = [
            `User Name: ${this.model.custName}`,
            `Order by: ${filterModelWrapper.userLeagueWrapper.orderByTitle}, ${filterModelWrapper.userLeagueWrapper.sortByTitle}`,
            `Sport Type: E-Sports`
        ];
        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
        }
        queryModel.filter = filterLines.join('\n');
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Top_Punter_LeagueGroup_${filterModelWrapper.searchByFileName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }

    private createQueryModel(): any {
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        return {
            periodType: filterModelWrapper.userLeagueModel.periodType,
            orderBy: filterModelWrapper.userLeagueModel.orderBy,
            sortBy: filterModelWrapper.userLeagueModel.sortBy,
            fromDate: filterModelWrapper.fromDateQueryString,
            toDate: filterModelWrapper.toDateQueryString,
            listCombinationId: filterModelWrapper.userLeagueWrapper.listCombinationId,
            custId: this.model.custId,
            listLeagueGroupId: filterModelWrapper.userLeagueWrapper.listLeagueGroupCombinationId,
            leagueGroupId: this.model.leagueGroupId
        };
    }
}
