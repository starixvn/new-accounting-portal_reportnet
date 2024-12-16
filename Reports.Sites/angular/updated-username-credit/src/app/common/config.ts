import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { PaginationConfig } from 'nap-components/pagination';

export class DateConfig {
    constructor(
        private _today: moment.Moment,
        private _balanceUpDate: moment.Moment = _today
    ) { }

    get today(): moment.Moment {
        return this._today;
    }

    get yesterday(): moment.Moment {
        return moment.utc(this._today).add(-1, 'day');
    }

    get balanceUpDate(): moment.Moment {
        return this._balanceUpDate;
    }
}

export let CONFIG = {
    date: new DateConfig(moment.utc().startOf('day')),
    dateFormatString: 'MMM DD, YYYY',
    dateFormatString2: 'MMM_DD_YYYY',
    dateMonthFormatString: 'MM, YYYY',
    dateMonthFormatString2: 'MM_YYYY'
};

@Injectable()
export class CustomPaginationConfig extends PaginationConfig {
    pageSizes = [50, 100, 200, 500];
    itemsPerPage = 50;
}
