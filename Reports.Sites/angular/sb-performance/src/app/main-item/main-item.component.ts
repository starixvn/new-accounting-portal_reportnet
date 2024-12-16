import { Component, EventEmitter, Input, Output } from '@angular/core';
import * as _ from 'lodash';
import { MainItemModel } from '../common/model';
import { SpecialSportConstant } from '../grid/grid.constant';
import { DrillDownModel } from './main-item.model';

@Component({
  selector: 'app-main-item',
  templateUrl: './main-item.component.html',
  styleUrls: ['./main-item.component.scss']
})
export class MainItemComponent {
  @Input() title: string;
  @Input() firstColumnHeader: string;
  @Input() isShowFullColumn: boolean;
  @Input() isChangedColumnName = false;
  @Input() isCheckedParlay = false;
  @Input() items: MainItemModel;
  @Output() drilDownClick = new EventEmitter<DrillDownModel>();
  sortColumnName = '';
  sortDirection = '';

  constructor(
  ) { }

  get hasData(): boolean {
    return this.items !== null && this.items.Entities.length > 0;
  }

  sort(sortColumnName: string) {
    if (this.sortColumnName !== sortColumnName) {
      this.sortDirection = '';
    }

    this.sortColumnName = sortColumnName;
    this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
    this.items.Entities = _.orderBy(this.items.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
  }

  isParlay(sport: string): boolean {
    if (_.isNil(sport)) {
      return false;
    }
    return this.isCheckedParlay ? sport.trim().toLowerCase() === SpecialSportConstant.PARLAY.toLowerCase() : false;
  }

  showGrid(id: string, name: string) {
    const model: DrillDownModel = {
      id,
      name,
    };
    this.drilDownClick.emit(model);
  }
}
