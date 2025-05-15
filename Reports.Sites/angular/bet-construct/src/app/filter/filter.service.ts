import { Injectable } from '@angular/core';
import { SelectItem } from 'nap-components/dropdown-select';
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

  getStatusSelectItems(): SelectItem[] {
    return [
        { text: 'All', value:  undefined },
        { text: 'Live', value: 1 },
        { text: 'Non-Live', value: 0 }
    ];
   }

    getDateTypeSelectItems(): SelectItem[] {
      return [
          { text: 'Win/Loss Date (GMT -4)', value: 0 },
          { text: 'Transaction Date - Time Zone:', value: 1 }
      ];
    }

   getGmtSelectItems(): SelectItem[] {
    return [
        { text: 'GMT +0', value: 0 },
        { text: 'GMT -4', value: -4 }
    ];
   }
}
