import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { LeagueGroup, LeagueGroupModel } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'leagueGroupTreeview'
})

export class LeagueGroupTreeviewPipe implements PipeTransform {
  transform(leagueGroups: LeagueGroupModel): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(leagueGroups)) {
      return undefined;
    }

    leagueGroups.LeagueGroups.forEach(leagueGroup => {
      const item = new TreeviewItem({ text: leagueGroup.GroupName, value: leagueGroup.LeagueIds, collapsed: true });
      var isUnCheck = leagueGroup.GroupName.includes('EURO') ||
      leagueGroup.GroupName.includes('World Cup') ||
      leagueGroup.GroupName.includes('Pingoal') ||
      leagueGroup.GroupName.includes('SABA Sport');
      var numberItems = leagueGroup.ChildItems.length;
      if(numberItems === 1) {
      treeItems.push(item);
      } else {
        leagueGroup.ChildItems.forEach(childItem => {
          var childTreeViewItem = new TreeviewItem ({text: childItem.GroupName, value: childItem.LeagueIds, checked: !isUnCheck});
          this.addItemForChildren(item, childTreeViewItem);
        });

        treeItems.push(item);
      }
    });

    return treeItems;
  }

  addItemForChildren(groupTreeviewItem: TreeviewItem, item: TreeviewItem): void {
    if (_.isNil(groupTreeviewItem.children)) {
      groupTreeviewItem.children = [item];
    } else {
      groupTreeviewItem.children.push(item);
    }
  }
}

