import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs-compat/Observable';
import { GridService } from '../grid.service';
import { UsernameDataModel } from './username-grid.model';

@Injectable({
  providedIn: 'root'
})
export class UsernameGridService {

  constructor(
    private httpService: HttpService,
    private gridService: GridService
) { }

query(): Observable<UsernameDataModel> {
    const queryModel = this.gridService.queryModel;
    return this.httpService.post<UsernameDataModel>(this.gridService.api, queryModel);
}
}
