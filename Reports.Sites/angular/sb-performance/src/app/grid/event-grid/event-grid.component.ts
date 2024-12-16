import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs-compat/Observable';
import { GridConstant, ReportTypeConstant } from '../grid.constant';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { EventDataModel } from './event-grid.model';
import { EventGridService } from './event-grid.service';

@Component({
  selector: 'app-event-grid',
  templateUrl: './event-grid.component.html',
  styleUrls: ['./event-grid.component.scss']
})
export class EventGridComponent implements Grid {

  data: EventDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private eventGridService: EventGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.eventGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
      return this.gridService.eventTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    get isShowNoMatchesAndNoBetTypeColumn(): boolean {
      return this.gridService.mainTitle !== ReportTypeConstant.TOP_LOSING_LEAGUES_TEXT;
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

    showMatchesOrBetTypes(id: string, event: string) {
      if (!this.isShowNoMatchesAndNoBetTypeColumn &&
          this.gridService.clickedFromColumn === GridConstant.CLICK_FROM_SITE) {
        this.gridService.matchTitle = event;
        this.gridService.generate(GridType.Match, { LiveIndicator: Number(id), ShowBy: 'Event', MatchId: null, BetType: null });
      } else {
        this.gridService.betTypeTitle = event;
        this.gridService.generate(GridType.BetType, { LiveIndicator: Number(id), ShowBy: 'Match', BetType: null });
      }
    }

    showBetTypes(id: string, event: string) {
      this.gridService.betTypeTitle = event;
      this.gridService.generate(GridType.BetType, { LiveIndicator: Number(id), ShowBy: 'BetType', MatchId: null });
    }

    showMatches(id: string, event: string) {
      this.gridService.matchTitle = event;
      this.gridService.generate(GridType.Match, { LiveIndicator: Number(id), ShowBy: 'Match', MatchId: null });
    }
}
