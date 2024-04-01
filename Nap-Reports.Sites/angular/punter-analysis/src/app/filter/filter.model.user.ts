import * as _ from 'lodash';
import { CommonUserLeagueFilterModel } from './filter.model.common-user-league';
import { PerformanceFilterModel } from './filter.model.performance';

export interface UserFilterModel extends CommonUserLeagueFilterModel, PerformanceFilterModel { }
