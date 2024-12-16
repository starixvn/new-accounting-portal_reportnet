import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs-compat/Observable';
import { GridService } from '../grid.service';
import { MatchDataModel } from './match-grid.model';

@Injectable({
  providedIn: 'root'
})
export class MatchGridService {

    constructor(
      private httpService: HttpService,
      private gridService: GridService
  ) { }

  query(): Observable<MatchDataModel> {
      const queryModel = this.gridService.queryModel;
      return this.httpService.post<MatchDataModel>(this.gridService.api, queryModel);
  }
}
