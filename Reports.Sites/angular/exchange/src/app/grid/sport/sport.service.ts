import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { ExchangeModel } from '../grid.model';
import { Helper } from 'nap-components/helper';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class SportService {

  constructor(
    private httpService: HttpService,
    private gridService: GridService
  ) { }

  query(): Observable<ExchangeModel> {
    const url = 'api/exchange/report';
    const queryModel = this.gridService.queryModel;
    this.gridService.cachedSportFilterModel = _.clone(this.gridService.queryModel);
    return this.httpService.post<ExchangeModel>(url, queryModel);
  }
  
  exportExcel(): void {
    const url = 'api/exchange/excel';
    const queryModel = this.gridService.cachedSportFilterModel;
    const fromDate =  queryModel.fromDate.replace(',', '').replace(/ /g, '_');
    const toDate = queryModel.toDate.replace(',', '').replace(/ /g, '_');
    this.httpService.download(url, queryModel).subscribe(response => {
        const fileName = `Exchange_WinLoss_report_From_${fromDate}_To_${toDate}.xlsx`;
        Helper.downloadFile(response, fileName);
    });
  }
}
