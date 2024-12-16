import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { LeagueDataModel } from './league-grid.model';
import { LeagueGridService } from './league-grid.service';
import { GridService } from '../grid.service';
import { ReportTypeConstant } from '../grid.constant';

@Component({
  selector: 'app-league-grid',
  templateUrl: './league-grid.component.html',
  styleUrls: ['./league-grid.component.scss']
})
export class LeagueGridComponent implements Grid {

  data: LeagueDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private leagueGridService: LeagueGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.leagueGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
      return this.gridService.leagueTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    get isHideActiveMember(): boolean {
      return this.gridService.mainTitle === ReportTypeConstant.TOP_WINNING_MEMBERS_TEXT;
    }

    sort(sortColumnName: string) {
      if (this.sortColumnName !== sortColumnName) {
        this.sortDirection = '';
      }

      this.sortColumnName = sortColumnName;
      this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
      this.data.Entities = _.orderBy(this.data.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
    }

    showEvent(id: string, event: string) {
      this.gridService.eventTitle = event;
      this.gridService.generate(GridType.Event, { LeagueId: Number(id), LiveIndicator: null, MatchId: null });
    }

}
