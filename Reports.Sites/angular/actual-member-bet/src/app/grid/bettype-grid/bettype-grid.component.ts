import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Grid } from '../grid.model';
import { GridService } from '../grid.service';
import { BettypeGridService } from './bettype-grid.service';
import { BettypeData } from './bettype-grid.model';

@Component({
    selector: 'app-bettype-grid',
    templateUrl: './bettype-grid.component.html',
    styleUrls: ['./bettype-grid.component.scss']
})
export class BettypeGridComponent implements Grid {

    bettypeData: BettypeData;

    constructor(
        private gridService: GridService,
        private bettypeService: BettypeGridService
    ) { }

    query() {
        this.bettypeService.query().subscribe(data => {
            this.bettypeData = data;
            this.gridService.showGridCompletedSubject.next(0);
        });
    }

    exportExcel() {
        this.bettypeService.exportExcel();
    }

    get hasExcel(): boolean {
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.bettypeData) && this.bettypeData.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.productName;
    }

}
