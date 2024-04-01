import * as moment from 'moment';

export interface UpdatedCreditModel {
    Year: number;
    Month: number;
    LogDate: moment.Moment;
    Department: string;
    Admin: string;
    Username: string;
    Level: string;
    Headline: string;
    Super: string;
    Master: string;
    Agent: string;
    OldValue: number;
    NewValue: number;
    Remark: string;
}
