import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { FilterModel } from '../filter/filter.model';
import { Observable } from 'rxjs';
import { SabaPromotion } from './grid.model';
import { CommonConstants } from '../common/constants';

@Injectable({
  providedIn: 'root'
})
export class GridService {

  constructor(private readonly httpService: HttpService) { }

  getSabaPromotions(filterModel: FilterModel): Observable<SabaPromotion> {
    const url = `${CommonConstants.API}/data`;
    return this.httpService.post<SabaPromotion>(url, filterModel);
  }
}
