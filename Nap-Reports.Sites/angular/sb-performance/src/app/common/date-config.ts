import * as moment from 'moment';

export class DateConfig {
    constructor(
        private _today: moment.Moment
    ) { }

    get today(): moment.Moment {
        return this._today;
    }

    get yesterday(): moment.Moment {
        return moment.utc(this._today).add(-1, 'day');
    }

    get minDate(): moment.Moment {
      return moment.utc(this._today).add(-2, 'day');
  }
}

export let CONFIG = {
    date: new DateConfig(moment.utc().startOf('day')),
    dateFormatString: 'MMM DD, YYYY',
    dateFormatString2: 'MMM_DD_YYYY',
    dateMonthFormatString: 'MM, YYYY',
    dateMonthFormatString2: 'MM_YYYY'
};
