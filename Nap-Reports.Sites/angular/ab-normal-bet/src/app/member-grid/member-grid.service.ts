import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { Observable } from 'rxjs';
import { MemberModel, QueryModel } from '../common/model';
import * as moment from 'moment';
import { QueryModelService } from './../common/query-model.service';
import { ExcelQueryModel } from './../common/model';
import { FilterModel } from '../filter/filter.model';

@Injectable()
export class MemberGridService {
  private url = 'api/ab-normal-bet/report/';

  constructor(
    private httpService: HttpService,
    private queryModelService: QueryModelService
  ) { }

  query(filterModel: FilterModel): Observable<MemberModel> {
    const url = this.url + 'member';
    const queryModel = this.createQueryModel(filterModel);
    return this.httpService.post<MemberModel>(url, queryModel);
  }

  exportExcel(filterModel: FilterModel) {
    const url = this.url + 'member-excel';
    const excelQueryModel = this.createExcelQueryModel(filterModel);
    excelQueryModel.pageIndex = 0;
    excelQueryModel.pageSize = 0;
    const fromDate = moment(excelQueryModel.fromDate).format('YYYY_MM_DD');
    const toDate = moment(excelQueryModel.toDate).format('YYYY_MM_DD');
    this.httpService.download(url, excelQueryModel).subscribe(response => {
      const fileName = `Abnormal_Bet_Void_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }

  private createQueryModel(filterModel: FilterModel): QueryModel {
    return this.queryModelService.createQueryModel(filterModel);
  }

  private createExcelQueryModel(filterModel: FilterModel): ExcelQueryModel {
    const title = `Abnormal Bet From ${filterModel.fromDate} To ${filterModel.toDate}`;
    return this.queryModelService.createExcelQueryModel(title, filterModel);
  }
}
