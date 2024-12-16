import * as _ from 'lodash';
import { SelectItem } from 'nap-components/dropdown-select';
import { NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { SearchBy } from './filter.model.main';
import { UserFilterModel } from './filter.model.user';
import { LeagueFilterModel } from './filter.model.league';
import { PerformanceFilterModel } from './filter.model.performance';


export interface FilterModel {
    searchBy: SearchBy;
    searchByOptions: SelectItem[];
    fromDate: NgbDateStruct;
    toDate: NgbDateStruct;
    user?: UserFilterModel;
    league?: LeagueFilterModel;
    performance?: PerformanceFilterModel;
    showLeagueGroupFilter: boolean;
}
