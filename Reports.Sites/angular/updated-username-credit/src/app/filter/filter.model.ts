import * as moment from 'moment';

export enum TimePeriod {
    Today,
    Yesterday,
    CurrentWeek,
    LastWeek,
    CurrentMonth,
    LastMonth,
    SinceLastMonth,
    SpecificDate,
    LastThreeMonths,
    LastSixMonths
}

export class FilterModel {
    constructor(public fromDate: moment.Moment, public toDate: moment.Moment, public userName: string, public reportBy: string) { }
}
