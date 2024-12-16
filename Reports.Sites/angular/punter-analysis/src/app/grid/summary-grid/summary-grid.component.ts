import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';
import { PaginationInstance } from 'nap-components/pagination';
import { GridService } from '../grid.service';
import { Grid, GridType } from '../grid.model';
import { SummaryData } from './summary-grid.model';
import { SummaryGridService } from './summary-grid.service';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { AssociatedAccountGridService } from '../associated-account-grid/associated-account-grid.service';
import { SportTypeGridService } from '../sport-type-grid/sport-type-grid.service';

@Component({
    selector: 'nap-summary-grid',
    templateUrl: './summary-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class SummaryGridComponent implements Grid {
    config: PaginationInstance;
    data: SummaryData;

    constructor(
        private gridService: GridService,
        private service: SummaryGridService,
        private betCountGridService: BetCountGridService,
        private associatedAccountGridService: AssociatedAccountGridService,
        private sportTypeGridService: SportTypeGridService
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
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        if (this.gridService.activeGrid === this) {
            return 'Main - Punter Analysis';
        } else {
            return 'Main';
        }
    }

    showSportType(custId: number, custName: string) {
        this.sportTypeGridService.model = this.service.createSportTypeGridModel(custId, custName);
        this.gridService.generate(GridType.SportType);
    }

    showBetCount(custId: number, custName: string): void {
        this.betCountGridService.model = this.service.createBetCountGridModel(custId, custName);
        this.gridService.generate(GridType.BetCount);
    }

    showAssociatedAccount(custId: number, custName: string): void {
        this.associatedAccountGridService.model = this.service.createAssociatedAccountGridModel(custId, custName);
        this.gridService.generate(GridType.AssociatedAccount);
    }
}
