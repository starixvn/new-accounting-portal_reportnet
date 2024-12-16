import { Component } from '@angular/core';
import { ExchangeLicenseeModel, Grid, GridType, QueryModel } from '../grid.model';
import { Observable } from 'rxjs';
import { LicenseeService } from './licensee.service';
import { GridService } from '../grid.service';
import * as _ from 'lodash';
import { BreadcrumbService } from '../../common/breadcrumb.service';

@Component({
  selector: 'app-licensee',
  templateUrl: './licensee.component.html',
  styleUrls: ['./licensee.component.scss'],
  providers: [
    BreadcrumbService
  ]
})
export class LicenseeComponent implements Grid {
  exchangeModel: ExchangeLicenseeModel;
  breadcrumbItems: string;
  originalEventType: string;

  constructor(
    private readonly licenseeService: LicenseeService,
    private readonly gridService: GridService,
    private breadcrumbService: BreadcrumbService
  ) {
    this.breadcrumbItems = this.breadcrumbService.createBreadcrumb(this.gridService.filterModel);
    this.originalEventType = this.gridService.filterModel.eventType;
   }

  title: string = 'Main';

  query(): Observable<ExchangeLicenseeModel> {
    return new Observable(observer => {
      this.licenseeService.query().subscribe(data => {
          this.exchangeModel = data;
          observer.complete();
      });
    });
  }

  exportExcel(): void {
    this.licenseeService.exportExcel();
  }

  showSports(licensee: string): void {
    this.gridService.sportTitle = licensee;
    const queryModel: QueryModel = {
      licenseeName: licensee,
      eventType: this.originalEventType,
      drillLevel: 1
    }
    this.gridService.generate(GridType.Sport, queryModel);
  }
}
