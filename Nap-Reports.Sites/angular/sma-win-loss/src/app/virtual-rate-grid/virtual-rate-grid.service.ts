import { VirtualRateModel } from './../common/model';
import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';

@Injectable()
export class VirtualRateGridService {
  private readonly url = 'api/sma-win-loss/virtual-rate';

  constructor(
    private readonly httpService: HttpService) { }

  query(custId: number): Observable<VirtualRateModel[]> {
    return this.httpService.post<VirtualRateModel[]>(this.url, { custId });
  }
}
