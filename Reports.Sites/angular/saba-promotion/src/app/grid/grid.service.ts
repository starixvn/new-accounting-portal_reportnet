import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { FilterModel } from '../filter/filter.model';
import { Observable } from 'rxjs';
import { SabaPromotion } from './grid.model';
import { CommonConstants } from '../common/constants';
import * as moment from 'moment';
import { Helper } from 'nap-components/helper';

@Injectable({
  providedIn: 'root'
})
export class GridService {

  constructor(private readonly httpService: HttpService) { }

  getSabaPromotions(filterModel: FilterModel): Observable<SabaPromotion> {
    const url = `${CommonConstants.API}/data`;
    return this.httpService.post<SabaPromotion>(url, filterModel);
  }

  exportExcel(filterModel: FilterModel): void {
    const fromDate = moment(filterModel.fromDate).format(CommonConstants.MM_DD_YYYY);
    const toDate = moment(filterModel.toDate).format(CommonConstants.MM_DD_YYYY);
    this.httpService.download(`${CommonConstants.API}/excel`, filterModel).subscribe(response => {
      const fileName = `SABA_Promotion_Report_From_${fromDate}_To_${toDate}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }
}
