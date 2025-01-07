import { ExcelQueryModel, SummaryEntity } from './grid.model';
import { Injectable } from "@angular/core";
import { HttpService } from "nap-components/http";
import { Helper } from 'nap-components/helper';
import { Observable } from 'rxjs/Observable';
import { FilterModel } from '../filter/filter.model';
import * as moment from 'moment';
import * as _ from 'lodash';

@Injectable()
export class GridService {
  constructor(
    private httpService: HttpService
  ) { }

  getData(fiterModel: FilterModel): Observable<SummaryEntity> {
    const url = 'api/sb-customer-performance-by-danger/report/data';
    return this.httpService.post<SummaryEntity>(url, fiterModel);
  }

  exportExcel(filterModel: FilterModel) {
    const url = 'api/sb-customer-performance-by-danger/report/excel';
    const excelQueryModel = this.createExcelQueryModel(filterModel);

    const fromDate = moment(excelQueryModel.fromDate).format('YYYY_MM_DD');
    const toDate = moment(excelQueryModel.toDate).format('YYYY_MM_DD');
    this.httpService.download(url, excelQueryModel).subscribe(response => {
      const fileName = `SB_Customer_Performance_By_Dangers_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }


  private createExcelQueryModel(filterModel: FilterModel): ExcelQueryModel {
    const title = `SB Customer Performance By Dangers From ${filterModel.fromDate} To ${filterModel.toDate}`;
    const excelQueryModel: ExcelQueryModel = _.extend({}, { title: title }, filterModel);
    return excelQueryModel;
  }
}
