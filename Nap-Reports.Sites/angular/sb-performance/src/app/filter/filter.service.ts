import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';

@Injectable()
export class FilterService {
  constructor(
    private readonly httpService: HttpService
  ) { }

  showFullColumn(): Observable<boolean> {
    return this.httpService.post<boolean>('api/sb-performance/check-full-view');
  }
}
