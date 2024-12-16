import * as moment from 'moment';

export class DateConfig {
  constructor(
    private _today: moment.Moment,
    private _balanceUpDate: moment.Moment = _today
  ) { }

  get today(): moment.Moment {
    return this._today;
  }

  get balanceUpDate(): moment.Moment {
    return this._balanceUpDate;
  }
}

export let CONFIG = {
  date: new DateConfig(moment.utc().startOf('day'), moment.utc().startOf('day')),
  dateFormatString: 'MMM DD, YYYY',
  dateFormatString2: 'MMM_DD_YYYY',
  dateMonthFormatString: 'MM, YYYY',
  dateMonthFormatString2: 'MM_YYYY'
};
