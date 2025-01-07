import * as moment from 'moment';
import { PaginationConfig } from 'nap-components/pagination';
import { Injectable } from '@angular/core';

export class DateConfig {
    constructor(
        private _today: moment.Moment
    ) { }

    get today(): moment.Moment {
        return this._today;
    }
}

export class Permission {
  constructor(
    private _permissionProfileId: number
) { }

get permissionProfileId(): number {
    return this._permissionProfileId;
  }
}

export let CONFIG = {
    date: new DateConfig(moment.utc().startOf('day')),
    dateFormatString: 'MMM DD, YYYY',
    dateFormatString2: 'MMM_DD_YYYY',
    dateMonthFormatString: 'MM, YYYY',
    dateMonthFormatString2: 'MM_YYYY',
    permission: new Permission(0)
};

@Injectable()
export class CustomPaginationConfig extends PaginationConfig {
    pageSizes = [1000, 2000, 3000, 4000, 5000];
    itemsPerPage = 1000;
}


