import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { BetFrom } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'betFromTreeview'
})

export class BetFromTreeviewPipe implements PipeTransform {
  transform(betFroms: BetFrom[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(betFroms)) {
      return undefined;
    }

    betFroms.forEach(betFrom => {
      const item = new TreeviewItem({ text: betFrom.Platform, value: betFrom.BetFromId });
      treeItems.push(item);
    });

    return treeItems;
  }
}

