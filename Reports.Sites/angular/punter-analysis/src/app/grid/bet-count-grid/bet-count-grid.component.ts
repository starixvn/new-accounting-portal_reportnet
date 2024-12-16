import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { PeriodType, FilterModelWrapper } from '../../filter/filter.model.main';
import { Grid } from '../grid.model';
import { GridService } from '../grid.service';
import { BetCountData } from './bet-count-grid.model';
import { BetCountGridService } from './bet-count-grid.service';

@Component({
    selector: 'nap-bet-count-grid',
    templateUrl: './bet-count-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class BetCountGridComponent implements Grid {
    periodType: PeriodType;
    data: BetCountData;

    constructor(
        private gridService: GridService,
        private service: BetCountGridService
    ) { }

    query(): Observable<any> {
        const filterModelWrapper = new FilterModelWrapper(this.gridService.filterModel);
        this.periodType = filterModelWrapper.userLeagueModel.periodType;
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
        return this.service.getBreadcrumbTitle();
    }
}
