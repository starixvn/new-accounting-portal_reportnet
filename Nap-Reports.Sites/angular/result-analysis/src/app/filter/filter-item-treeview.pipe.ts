import { Pipe, PipeTransform } from '@angular/core';
import { TreeviewItem } from 'ngx-treeview';
import { LeagueModel } from './filter.model';
import * as _ from 'lodash';

@Pipe({
  name: 'leagueGroupTreeviewItem'
})
export class FilterItemTreeviewPipe implements PipeTransform {

  transform(values: LeagueModel[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (values.length === 0) {
      return [];
    }

    values = _.uniqBy(values, 'GroupId');
    const specialGroup = new TreeviewItem({ text: 'Specials', value: 0});
    const nonGroup = new TreeviewItem({ text: 'Non-group Leagues', value: -1}); 

    values.forEach(data => {
      const item = new TreeviewItem({ text: data.GroupName, value: data.GroupId });
      let groupTreeviewItem: TreeviewItem;
      if (this.isSpecialLeague(data)) {
        groupTreeviewItem = specialGroup;
        item.checked = false;
        if (_.isNil(groupTreeviewItem.children)) {
          groupTreeviewItem.children = [item];
        } else {
          groupTreeviewItem.children.push(item);
        }
      } else {
        if (data.GroupId !== -1) {
          item.checked = true;
          treeItems.push(item);
        }
      }
    });
    treeItems.push(specialGroup);
    treeItems.push(nonGroup);
    return treeItems;
  }

  private isSpecialLeague(league: LeagueModel): boolean {
    return league.GroupName.toLowerCase().search('specials') > -1
      || league.GroupName.toLowerCase().search('specific 10min') > -1
      || league.GroupName.toLowerCase().search('specific 15min') > -1
  }
}
