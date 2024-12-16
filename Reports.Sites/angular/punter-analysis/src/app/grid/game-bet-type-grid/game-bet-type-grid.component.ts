import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { Grid, GridType, LiveStatus } from '../grid.model';
import { GridService } from '../grid.service';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { LeagueGridService } from '../league-grid/league-grid.service';
import { GameBetTypeGridService } from './game-bet-type-grid.service';
import { GameBetTypeData } from './game-bet-type-grid.model';

@Component({
    selector: 'nap-game-bet-type-grid',
    templateUrl: './game-bet-type-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class GameBetTypeGridComponent implements Grid {
    LiveStatus = LiveStatus;
    data: GameBetTypeData;

    constructor(
        private gridService: GridService,
        private service: GameBetTypeGridService,
        private betCountGridService: BetCountGridService,
        private leagueGridService: LeagueGridService
    ) { }

    query(): Observable<any> {
        return Observable.create(observer => {
            this.service.query().subscribe(data => {
                this.data = data;
                observer.complete();
            });
        });
    }

    exportExcel() {
        this.service.exportExcel();
    }

    get hasExcel(): boolean {
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.service.model.betTypeGroupName;
    }

    showBetCount(betTypeId: number, betTypeName: string, liveStatusId = LiveStatus.Both) {
        this.betCountGridService.model = this.service.createBetCountGridService(betTypeId, betTypeName, liveStatusId);
        this.gridService.generate(GridType.BetCount);
    }

    showLeague(betTypeId: number, betTypeName: string, liveStatusId = LiveStatus.Both) {
        this.leagueGridService.model = this.service.createLeagueGridService(betTypeId, betTypeName, liveStatusId);
        this.gridService.generate(GridType.League);
    }
}
