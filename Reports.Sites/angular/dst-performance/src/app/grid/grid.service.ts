import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';
import { HttpService } from 'nap-components/http';
import { ReportModel } from '../common/model';
import { Observable } from 'rxjs';
import { CommonConstants } from '../common/constants';
import * as moment from 'moment';
import { Helper } from 'nap-components/helper';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  filterModel!: FilterModel;
  activeGrid!: Grid;
  generate: ((gridType: GridType) => void) | undefined;

  constructor(private httpService: HttpService) { }

  getData(filterModel: FilterModel): Observable<ReportModel> {
    return this.httpService.post<ReportModel>(`${CommonConstants.API}/data`, filterModel);
  }

  exportExcel(filterModel: FilterModel): void {
    const fromDate = moment(filterModel.fromDate).format(CommonConstants.MM_DD_YYYY);
    const toDate = moment(filterModel.toDate).format(CommonConstants.MM_DD_YYYY);
    this.httpService.download(`${CommonConstants.API}/excel-data`, filterModel).subscribe(response => {
      const fileName = `DST_Performance_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }
}
