import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { Sport } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'sportTreeview'
})

export class SportTreeviewPipe implements PipeTransform {
  transform(sports: Sport[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(sports)) {
      return undefined;
    }

    sports.forEach(sport => {
      const item = new TreeviewItem({ text: sport.SportGroup, value: sport.SportIds });
      treeItems.push(item);
    });

    return treeItems;
  }
}

