import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { PromotionType } from './filter.model';
import { CommonConstants } from '../common/constants';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  constructor(private readonly httpService: HttpService) { }

  getPromotionTypes(): Observable<PromotionType[]> {
    const url = `${CommonConstants.API}/promotion-type`;
    return this.httpService.post<PromotionType[]>(url);
  }
}
