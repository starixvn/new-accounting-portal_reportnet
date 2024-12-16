import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';

@Injectable()
export class GridFilterService {
    filterModel: FilterModel;
}
