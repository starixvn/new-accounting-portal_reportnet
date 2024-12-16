import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { FilterModel } from '../../../app/filter/filter.model';
import { CommonConfig } from '../../../app/common/constants';
import { EventStatusLevelModel, ExcelQueryModel, TicketViewModel } from '../../../app/common/model';
import * as moment from 'moment';
import { Helper } from 'nap-components/helper';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class EventStatusService {

  constructor(private httpService: HttpService) { }

  getTickets(fiterModel: FilterModel): Observable<TicketViewModel<EventStatusLevelModel>> {
    return this.httpService.post<TicketViewModel<EventStatusLevelModel>>(`${CommonConfig.API}event-status-level`, fiterModel);
  }

  exportExcel(filterModel: FilterModel): void {
    const excelQueryModel = this.createExcelQueryModel(filterModel);

    const fromDate = moment(excelQueryModel.startDate).format('YYYY_MM_DD');
    const toDate = moment(excelQueryModel.endDate).format('YYYY_MM_DD');
    this.httpService.download(`${CommonConfig.API}excel-event-status`, excelQueryModel).subscribe(response => {
      const fileName = `Cash_Out_Performance_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }

  private createExcelQueryModel(filterModel: FilterModel): ExcelQueryModel {
    const title = `Cash Out Performance Report From ${filterModel.startDate} To ${filterModel.endDate}`;
    const excelQueryModel: ExcelQueryModel = _.extend({}, { title }, filterModel);
    excelQueryModel.pageIndex = 0;
    excelQueryModel.pageSize = 0;
    return excelQueryModel;
  }
}
