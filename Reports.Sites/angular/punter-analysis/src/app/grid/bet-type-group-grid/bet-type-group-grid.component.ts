import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { GridService } from '../grid.service';
import { Grid, GridType } from '../grid.model';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { GameBetTypeGridService } from './../game-bet-type-grid/game-bet-type-grid.service';
import { BetTypeGroupGridService } from './bet-type-group-grid.service';
import { BetTypeGroupData } from './bet-type-group-grid.model';

@Component({
    selector: 'nap-bet-type-group-grid',
    templateUrl: './bet-type-group-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class BetTypeGroupGridComponent implements Grid {
    data: BetTypeGroupData;

    constructor(
        private gridService: GridService,
        private service: BetTypeGroupGridService,
        private betCountGridService: BetCountGridService,
        private gameBetTypeGridService: GameBetTypeGridService
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
        return this.service.model.leagueGroupName;
    }

    showBetType(betTypeGroupId: number, betTypeGroupName: string) {
        this.gameBetTypeGridService.model = this.service.createGameBetTypeGridModel(betTypeGroupId, betTypeGroupName);
        this.gridService.generate(GridType.GameBetType);
    }

    showBetCount(betTypeGroupId: number, betTypeGroupName: string): void {
        this.betCountGridService.model = this.service.createBetCountGridModel(betTypeGroupId, betTypeGroupName);
        this.gridService.generate(GridType.BetCount);
    }
}
