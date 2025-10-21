import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { Helper } from 'nap-components/helper';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { CommonConstants } from 'src/app/common/constants';
import { SportModel } from 'src/app/common/model';
import { FilterModel } from 'src/app/filter/filter.model';

@Injectable({
  providedIn: 'root'
})
export class SportGridService {

  constructor(private httpService: HttpService) { }

  getSports(fiterModel: FilterModel): Observable<SportModel> {
    return this.httpService.post<SportModel>(`${CommonConstants.API}/sports`, fiterModel);
  }

  exportExcel(filterModel: FilterModel): void {
    const fromDate = moment(filterModel.fromDate).format(CommonConstants.DD_MM_YYYY);
    const toDate = moment(filterModel.toDate).format(CommonConstants.DD_MM_YYYY);
    this.httpService.download(`${CommonConstants.API}/excel-sports`, filterModel).subscribe(response => {
      const fileName = `BetConstruct_Performance_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }
}
