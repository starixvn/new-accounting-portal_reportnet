import * as moment from 'moment';

export interface UpdatedUsernameModel {
    Year: number;
    Month: number;
    LogDate: moment.Moment;
    Department: string;
    Admin: string;
    CurrentUsername: string;
    Level: string;
    Headline: string;
    Super: string;
    Master: string;
    Agent: string;
    FromUsername: number;
    ToUsername: number;
}
