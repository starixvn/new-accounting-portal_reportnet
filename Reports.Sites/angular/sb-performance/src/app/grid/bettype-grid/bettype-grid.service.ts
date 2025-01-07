import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs-compat/Observable';
import { GridService } from '../grid.service';
import { BetTypeDataModel } from './bettype-grid.model';

@Injectable({
  providedIn: 'root'
})
export class BettypeGridService {

    constructor(
      private httpService: HttpService,
      private gridService: GridService
  ) { }

  query(): Observable<BetTypeDataModel> {
      const queryModel = this.gridService.queryModel;
      return this.httpService.post<BetTypeDataModel>(this.gridService.api, queryModel);
  }

}
