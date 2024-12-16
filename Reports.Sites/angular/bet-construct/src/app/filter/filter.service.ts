import { Injectable } from '@angular/core';
import { SelectItem } from 'nap-components/dropdown-select';
import { CommonConstants } from '../common/constants';
import { TurnoverType } from '../common/model';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private readonly url = 'api/cash-out-performance/';

  constructor() { }

  getTurnoverOptions(): SelectItem[] {
    return [{
        text: 'Normal',
        value: TurnoverType.Normal
    }, {
        text: 'No Draw',
        value: TurnoverType.NoDraw
    }];
  }
}
