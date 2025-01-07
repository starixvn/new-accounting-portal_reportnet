import { Pipe, PipeTransform } from '@angular/core';
import { TreeviewItem } from 'ngx-treeview';
import { PromotionType } from './filter.model';

@Pipe({
  name: 'promotionTypeTreeviewItem'
})
export class FilterItemTreeviewPipe implements PipeTransform {

  transform(values: PromotionType[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (values.length === 0) {
      return [];
    }

    values.forEach(data => {
      treeItems.push(new TreeviewItem({ text: data.BetTypeName, value: data.BetTypeId, checked: true }));
    });
    return treeItems;
  }
}
