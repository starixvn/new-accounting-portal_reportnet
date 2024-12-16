import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { Currency, Site, Odds } from './filter.model';

@Injectable()
export class FilterService {
  constructor(
    private httpService: HttpService
  ) { }

  getCurrency(): Observable<Currency[]> {
    const url = 'api/actual-member-bet/filter/currency';
    return this.httpService.post<Currency[]>(url);
  }

  getSite(): Observable<Site[]> {
    const url = 'api/actual-member-bet/filter/site';
    return this.httpService.post<Site[]>(url);
  }

  getOdds(): Observable<Odds[]> {
    const url = 'api/actual-member-bet/filter/odds';
    return this.httpService.post<Odds[]>(url);
  }
}
