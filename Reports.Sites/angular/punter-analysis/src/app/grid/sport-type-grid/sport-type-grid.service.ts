import { GameGridModel } from './../game-grid/game-grid.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { SportTypeGridModel } from './sport-type-grid.model';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { BetTypeGridModel } from '../bet-type-grid/bet-type-grid.model';

@Injectable()
export class SportTypeGridService {
    private url = 'api/punter-analysis/sport-type';
    model: SportTypeGridModel;

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    createBetTypeGridModel(sportId: number, sportName: string, leagueGroupId: number): BetTypeGridModel {
        return _.extend(this.model, {
            sportId: sportId,
            sportName: sportName,
            leagueGroupId: leagueGroupId
        });
    }

    createGameGridModel(sportId: number, sportName: string, leagueGroupId): GameGridModel {
        return _.extend(this.model, {
            custId: this.model.custId,
            custName: this.model.custName,
            sportId: sportId,
            sportName: sportName,
            leagueGroupId: leagueGroupId
        });
    }

    createBetCountGridModel(sportId: number, sportName: string, leagueGroupId: number): BetCountGridModel {
        return {
            fromGridType: GridType.SportType,
            custId: this.model.custId,
            custName: this.model.custName,
            sportId: sportId,
            sportName: sportName,
            leagueGroupId: leagueGroupId
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
            `Sport Type: ${filterModelWrapper.selectedSportTypes}`
        ];
        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
        }
        queryModel.filter = filterLines.join('\n');
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Top_Punter_Sporttype_${filterModelWrapper.searchByFileName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
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
            leagueId: filterModelWrapper.leagueId,
            custId: this.model.custId,
            listLeagueGroupId: filterModelWrapper.userLeagueWrapper.listLeagueGroupCombinationId
        };
    }
}
