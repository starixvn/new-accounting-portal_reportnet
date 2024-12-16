import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { ResultAnalysisModel } from './grid.model';
import { FilterModel } from '../filter/filter.model';
import { CommonConstants } from '../common/constants';
import { Helper } from 'nap-components/helper';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class GridService {

  constructor(private readonly httpService: HttpService) { }

  getData(filter: FilterModel): Observable<ResultAnalysisModel> {
    return this.httpService.post<ResultAnalysisModel>(`${CommonConstants.API}/result-analysis`, filter);
  }

  exportExcel(filterModel: FilterModel): void {
    this.httpService.download(`${CommonConstants.API}/export-excel`, filterModel).subscribe(response => {
      const fileName =
      `Result_Analysis_Report_From_${this.formatDate(filterModel.fromDate)}_To_${this.formatDate(filterModel.toDate)}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }

  private formatDate(date: string): string{
    return moment(date).format('MM_DD_YYYY');
  }
}
