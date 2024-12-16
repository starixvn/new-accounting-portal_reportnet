import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private readonly httpService: HttpService) { }

  getToday(): Observable<moment.Moment> {
    return this.httpService.post<moment.Moment>('state-api/application/today');
  }
}
