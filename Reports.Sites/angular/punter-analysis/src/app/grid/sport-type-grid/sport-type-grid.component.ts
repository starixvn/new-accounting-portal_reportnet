import { GameGridService } from './../game-grid/game-grid.service';
import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { GridService } from '../grid.service';
import { Grid, GridType } from '../grid.model';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { BetTypeGridService } from '../bet-type-grid/bet-type-grid.service';
import { SportTypeData, SportTypeItem } from './sport-type-grid.model';
import { SportTypeGridService } from './sport-type-grid.service';

@Component({
  selector: 'nap-sport-type-grid',
  templateUrl: './sport-type-grid.component.html',
  styleUrls: ['../grid.component.scss']
})
export class SportTypeGridComponent implements Grid {
  data: SportTypeData;

  constructor(
    private gridService: GridService,
    private service: SportTypeGridService,
    private betCountGridService: BetCountGridService,
    private betTypeGridService: BetTypeGridService,
    private gameGridService: GameGridService
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
    return this.service.model.custName;
  }

  showBetType(sportId: number, sportName: string, leagueGroupId: number) {
    if (sportId === 43 && this.gridService.filterModel.league === undefined) {
      this.gameGridService.model = this.service.createGameGridModel(sportId, sportName, leagueGroupId);
      this.gridService.generate(GridType.Game);
    } else {
      this.betTypeGridService.model = this.service.createBetTypeGridModel(sportId, sportName, leagueGroupId);
      this.gridService.generate(GridType.BetType);
    }
  }

  showBetCount(sportId: number, sportName: string, leagueGroupId: number): void {
    let leagueGroupIdValue = leagueGroupId;

    this.betCountGridService.model = this.service.createBetCountGridModel(sportId, sportName, leagueGroupIdValue);
    this.gridService.generate(GridType.BetCount);
  }
}
