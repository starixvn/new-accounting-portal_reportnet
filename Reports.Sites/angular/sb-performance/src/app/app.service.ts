import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs/Observable';
import * as moment from 'moment';

@Injectable()
export class AppService {
  constructor(
    private httpService: HttpService
  ) { }

  getToday(): Observable<moment.Moment> {
    return this.httpService.post<moment.Moment>('state-api/application/today');
  }
}
