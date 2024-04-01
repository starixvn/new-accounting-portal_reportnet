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
      const sportItem = new TreeviewItem({ text: sport.SportName, value: sport.SportType });

      sport.BettypeGroups.forEach(betTypeGroup => {
        var betTypeGroupItem = new TreeviewItem({ text: betTypeGroup.BettypeGroupName, value: betTypeGroup.BettypeGroupName });
        betTypeGroup.Bettypes.forEach(betType => {
          var item = new TreeviewItem({ text: betType.BettypeName, value: betType.Bettype });
          if (_.isNil(betTypeGroupItem.children)) {
            betTypeGroupItem.children = [item];
          } else {
            betTypeGroupItem.children.push(item);
          }
        });

        if (_.isNil(sportItem.children)) {
          sportItem.children = [betTypeGroupItem];
        } else {
          sportItem.children.push(betTypeGroupItem);
        }
      });

      treeItems.push(sportItem);
    });

    return treeItems;
  }
}

