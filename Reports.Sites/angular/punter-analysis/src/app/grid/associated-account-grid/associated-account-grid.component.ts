import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { PaginationInstance } from 'nap-components/pagination';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { AssociatedAccountData } from './associated-account-grid.model';
import { AssociatedAccountGridService } from './associated-account-grid.service';
import { SportTypeGridService } from '../sport-type-grid/sport-type-grid.service';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';

@Component({
    selector: 'nap-associated-account-grid',
    templateUrl: './associated-account-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class AssociatedAccountGridComponent implements Grid {
    config: PaginationInstance;
    data: AssociatedAccountData;

    constructor(
        private gridService: GridService,
        private service: AssociatedAccountGridService,
        private sportTypeGridService: SportTypeGridService,
        private betCountGridService: BetCountGridService
    ) { }

    query(): Observable<any> {
        return Observable.create(observer => {
            this.service.query().subscribe(data => {
                this.data = data;
                this.config = {
                    id: 'custom',
                    itemsPerPage: 100,
                    currentPage: 1,
                    totalItems: this.data.Entities.length
                };
                observer.complete();
            });
        });
    }

    exportExcel() {
        this.service.exportExcel();
    }

    get hasExcel(): boolean {
        return true;
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return `Associated accounts of Punter ${this.service.model.custName}`;
    }

    showSportType(custId: number, custName: string) {
      this.sportTypeGridService.model = this.service.createSportTypeGridModel(custId, custName);
      this.gridService.generate(GridType.SportType);
  }

  showBetCount(custId: number, custName: string): void {
    this.betCountGridService.model = this.service.createBetCountGridModel(custId, custName);
    this.gridService.generate(GridType.BetCount);
}
}
