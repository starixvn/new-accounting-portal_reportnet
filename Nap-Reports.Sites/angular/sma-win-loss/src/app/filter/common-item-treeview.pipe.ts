import { Pipe, PipeTransform } from '@angular/core';
import { TreeviewItem } from 'ngx-treeview';
import { CommonItem } from './filter.model';
import * as _ from 'lodash';

@Pipe({
  name: 'commonItemTreeview'
})
export class CommonItemTreeviewPipe implements PipeTransform {

  transform(items: CommonItem[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(items)) {
      return undefined;
    }

    items.forEach(item => {
      const data = new TreeviewItem({ text: item.Text, value: item.Value });
      treeItems.push(data);
    });

    return treeItems;
  }

}
