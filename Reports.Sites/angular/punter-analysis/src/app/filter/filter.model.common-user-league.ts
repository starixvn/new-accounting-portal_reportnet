import * as _ from 'lodash';
import { DownlineTreeviewItem } from 'ngx-treeview';
import { SelectItem } from 'nap-components/dropdown-select';
import { SortBy } from 'nap-components/model';
import { OrderBy, PeriodType } from './filter.model.main';

export interface CommonUserLeagueFilterModel {
    topRow: number;
    orderBy: OrderBy;
    orderByOptions: SelectItem[];
    sortBy: SortBy;
    sortByOptions: SelectItem[];
    periodType: PeriodType;
    selectedBetTypeTreeviewItems: DownlineTreeviewItem[];
    selectedLeagueGroupTreeviewItems: DownlineTreeviewItem[];
}
