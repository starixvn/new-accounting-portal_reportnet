import { Pipe, PipeTransform } from '@angular/core';
import { TreeviewItem } from 'ngx-treeview';
import { FilterGroupItemModel, FilterItemModel } from './filter.model';
import * as _ from 'lodash';

@Pipe({
  name: 'filterItemTreeview'
})
export class FilterItemTreeviewPipe implements PipeTransform {

  transform(filterModels: FilterItemModel[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(filterModels)) {
      return undefined;
    }

    filterModels.forEach(itemModel => {
      const item = new TreeviewItem({ text: itemModel.Name, value: itemModel.Id });
      treeItems.push(item);
    });

    return treeItems;
  }

}

@Pipe({
  name: 'filterGroupItemTreeview'
})
export class FilterGroupItemTreeviewPipe implements PipeTransform {

  transform(filterModels: FilterGroupItemModel[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(filterModels)) {
      return undefined;
    }

    filterModels.forEach(itemModel => {
      const item = new TreeviewItem({ text: itemModel.GroupName, value: '', collapsed: true,
        children: this.getChildrens(itemModel.GroupId, itemModel.ChildItems) });
      treeItems.push(item);
    });

    return treeItems;
  }

  private getChildrens(groupId: number, data: FilterItemModel[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];
    data.forEach(item => {
      treeItems.push(new TreeviewItem({ text: item.Name, value: `${groupId}_${item.Id}` }));
    });

    return treeItems;
  }
}

@Pipe({
  name: 'filterItemNameTreeview'
})
export class FilterItemNameTreeviewPipe implements PipeTransform {

  transform(filterModels: FilterItemModel[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(filterModels)) {
      return undefined;
    }

    filterModels.forEach(itemModel => {
      const item = new TreeviewItem({ text: itemModel.Name, value: itemModel.Name });
      treeItems.push(item);
    });

    return treeItems;
  }

}
