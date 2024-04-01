import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { GridService } from '../grid.service';
import { Grid, GridType } from '../grid.model';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { BetTypeGroupGridService } from './../bet-type-group-grid/bet-type-group-grid.service';
import { GameData } from './game-grid.model';
import { GameGridService } from './game-grid.service';

@Component({
    selector: 'nap-game-grid',
    templateUrl: './game-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class GameGridComponent implements Grid {
    data: GameData;

    constructor(
        private gridService: GridService,
        private service: GameGridService,
        private betCountGridService: BetCountGridService,
        private betTypeGroupGridService: BetTypeGroupGridService
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
        return this.service.model.sportName;
    }

    showBetTypeGroup(leagueGroupId: number, leagueGroupName: string) {
        this.betTypeGroupGridService.model = this.service.createBetTypeGroupGridModel(leagueGroupId, leagueGroupName);
        this.gridService.generate(GridType.BetTypeGroup);
    }

    showBetCount(leagueGroupId: number, leagueGroupName: string): void {
        this.betCountGridService.model = this.service.createBetCountGridModel(leagueGroupId, leagueGroupName);
        this.gridService.generate(GridType.BetCount);
    }
}
