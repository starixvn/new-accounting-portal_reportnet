import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { LeagueGroup } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'leagueGroupTreeview'
})

export class LeagueGroupTreeviewPipe implements PipeTransform {
  transform(leagueGroups: LeagueGroup[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(leagueGroups)) {
      return undefined;
    }

    leagueGroups.forEach(leagueGroup => {
      const item = new TreeviewItem({ text: leagueGroup.GroupName, value: leagueGroup.GroupId });
      treeItems.push(item);
    });

    return treeItems;
  }
}

