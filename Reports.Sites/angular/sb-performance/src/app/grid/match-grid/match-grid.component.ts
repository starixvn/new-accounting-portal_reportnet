import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs-compat/Observable';
import { GridConstant, ReportTypeConstant } from '../grid.constant';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { MatchDataModel } from './match-grid.model';
import { MatchGridService } from './match-grid.service';

@Component({
  selector: 'app-match-grid',
  templateUrl: './match-grid.component.html',
  styleUrls: ['./match-grid.component.scss']
})
export class MatchGridComponent implements Grid {
  data: MatchDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private matchGridService: MatchGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.matchGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.matchTitle;
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

    showBetTypeOrEvent(id: string, match: string) {
      if (this.gridService.mainTitle === ReportTypeConstant.TOP_LOSING_LEAGUES_TEXT
          && this.gridService.clickedFromColumn === GridConstant.CLICK_FROM_MATCHES) {
        this.gridService.eventTitle = match;
        this.gridService.generate(GridType.Event, { ShowBy: 'Match', MatchId: Number(id), LiveIndicator: null, BetType: null });
      } else {
        this.gridService.betTypeTitle = match;
        this.gridService.generate(GridType.BetType, { ShowBy: 'Match', MatchId: Number(id), BetType: null });
      }

    }
}
