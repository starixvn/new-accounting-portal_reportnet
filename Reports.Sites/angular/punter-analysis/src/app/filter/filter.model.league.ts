import { IOption } from 'nap-components/select';
import { CommonUserLeagueFilterModel } from './filter.model.common-user-league';

export interface LeagueFilterModel extends CommonUserLeagueFilterModel {
    leagueId: string;
    leagueName: string;
    sportType: number;
    sportOptions: IOption[];
}
