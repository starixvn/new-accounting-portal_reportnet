import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { SiteModel } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'siteTreeview'
})

export class SiteTreeviewPipe implements PipeTransform {
  transform(sites: SiteModel[]): TreeviewItem[] {
    const creditGroup = 'Credit';
    const creditSites = new TreeviewItem({ text: 'Credit', value: '0' });
    const depoSitSites = new TreeviewItem({ text: 'Deposit', value: '1' });
    let siteLabelsOfCredit: string[] = [];

    if (_.isNil(sites)) {
      return undefined;
    }

    sites.forEach(site => {
      const item = new TreeviewItem({ text: site.SiteName, value: site.SiteName });
      let groupTreeviewItem: TreeviewItem;

      if (site.SiteGroup === creditGroup) {
        groupTreeviewItem = creditSites;
        const indexOfSiteLabel = siteLabelsOfCredit.indexOf(site.SiteLabel);
        if (siteLabelsOfCredit.length === 0 || indexOfSiteLabel === -1)
        {
          siteLabelsOfCredit.push(site.SiteLabel);
          this.addItemForChildren(groupTreeviewItem, new TreeviewItem({ text: site.SiteLabel, value: '0' }));
          this.addItemForChildren(groupTreeviewItem.children[groupTreeviewItem.children.length-1], item);
        } else {
          this.addItemForChildren(groupTreeviewItem.children[indexOfSiteLabel], item);
        }
      } else {
        
        groupTreeviewItem = depoSitSites;
        this.addItemForChildren(groupTreeviewItem, item);
      }
    });

    const siteTreeItems = [
      creditSites,
      depoSitSites
    ];

    return siteTreeItems.filter(item => !_.isNil(item.children) && item.children.length > 0);
  }

  addItemForChildren(groupTreeviewItem: TreeviewItem, item: TreeviewItem): void {
    if (_.isNil(groupTreeviewItem.children)) {
      groupTreeviewItem.children = [item];
    } else {
      groupTreeviewItem.children.push(item);
    }
  }
}

