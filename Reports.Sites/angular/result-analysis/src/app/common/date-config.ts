import * as moment from "moment";

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
}

export const CONFIG = {
    date: new DateConfig(moment.utc().startOf('day'))
};
