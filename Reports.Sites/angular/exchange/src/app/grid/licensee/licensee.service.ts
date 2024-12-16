import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { GridService } from '../grid.service';
import { ExchangeLicenseeModel } from '../grid.model';
import { Helper } from 'nap-components/helper';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class LicenseeService {

  constructor(
    private httpService: HttpService,
    private gridService: GridService
  ) { }

  query(): Observable<ExchangeLicenseeModel> {
    const url = 'api/exchange/main-report';
    const queryModel = this.gridService.queryModel;
    this.gridService.cachedLicenseeFilterModel = _.clone(this.gridService.queryModel);
    return this.httpService.post<ExchangeLicenseeModel>(url, queryModel);
  }

  exportExcel(): void {
    const url = 'api/exchange/main-excel';
    const queryModel = this.gridService.cachedLicenseeFilterModel;
    const fromDate =  queryModel.fromDate.replace(',', '').replace(/ /g, '_');
    const toDate = queryModel.toDate.replace(',', '').replace(/ /g, '_');
    this.httpService.download(url, queryModel).subscribe(response => {
        const fileName = `Exchange_WinLoss_report_From_${fromDate}_To_${toDate}.xlsx`;
        Helper.downloadFile(response, fileName);
    });
  }
}
