import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { LiveStatus, GridType } from '../grid.model';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { LeagueGridModel } from '../league-grid/league-grid.model';
import { GameBetTypeGridModel, GameBetTypeData } from './game-bet-type-grid.model';

@Injectable()
export class GameBetTypeGridService {
    private url = 'api/punter-analysis/game-bet-type';
    model: GameBetTypeGridModel;

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    createBetCountGridService(betTypeId: number, betTypeName: string, liveStatusId: LiveStatus): BetCountGridModel {
        return {
            fromGridType: GridType.GameBetType,
            custId: this.model.custId,
            custName: this.model.custName,
            sportId: this.model.sportId,
            sportName: this.model.sportName,
            leagueGroupId: this.model.leagueGroupId,
            leagueGroupName: this.model.leagueGroupName,
            betTypeGroupId: this.model.betTypeGroupId,
            betTypeGroupName: this.model.betTypeGroupName,
            betTypeId: betTypeId,
            betTypeName: betTypeName,
            liveStatusId: liveStatusId
        };
    }

    createLeagueGridService(betTypeId: number, betTypeName: string, liveStatusId: LiveStatus): LeagueGridModel {
        return _.extend(this.model, {
            betTypeId: betTypeId,
            betTypeName: betTypeName,
            liveStatusId: liveStatusId
        });
    }

    query(): Observable<GameBetTypeData> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<GameBetTypeData>(this.url, queryModel);
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
            `Sport Type: E-Sports`,
            `League Group: ${this.model.leagueGroupName}`,
            `Bet Type Group: ${this.model.betTypeGroupName}`
        ];
        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
        }
        queryModel.filter = filterLines.join('\n');
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Top_Punter_Bettype_${filterModelWrapper.searchByFileName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
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
            leagueGroupId: this.model.leagueGroupId,
            betTypeGroupId: this.model.betTypeGroupId
        };
    }
}
