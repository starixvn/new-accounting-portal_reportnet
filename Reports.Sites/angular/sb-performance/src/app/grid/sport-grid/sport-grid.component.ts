import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { ReportTypeConstant, SpecialSportConstant } from '../grid.constant';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { SportDataModel } from './sport-grid.model';
import { SportGridService } from './sport-grid.service';

@Component({
  selector: 'app-sport-grid',
  templateUrl: './sport-grid.component.html',
  styleUrls: ['./sport-grid.component.scss']
})
export class SportGridComponent implements Grid {
  data: SportDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private sportGridService: SportGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.sportGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.sportTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    get isHideActiveMember(): boolean {
      return this.gridService.mainTitle === ReportTypeConstant.TOP_WINNING_MEMBERS_TEXT;
    }

    isParlay(sport: string): boolean {
      if (_.isNil(sport)) {
        return false;
      }
      return sport.trim().toLowerCase() === SpecialSportConstant.PARLAY.toLowerCase();
    }

    sort(sortColumnName: string) {
      if (this.sortColumnName !== sortColumnName) {
        this.sortDirection = '';
      }

      this.sortColumnName = sortColumnName;
      this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
      this.data.Entities = _.orderBy(this.data.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
    }

    showLeague(id: string, sport: string) {
      this.gridService.leagueTitle = sport;
      this.gridService.generate(GridType.League, { SportId: Number(id), LeagueId: null, LiveIndicator: null, MatchId: null, ShowBy: null });
    }
}
