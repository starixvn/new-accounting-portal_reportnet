import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'lodash';
import { Currency } from './filter.model';
import { TreeviewItem } from 'ngx-treeview';

@Pipe({
  name: 'currencyTreeview'
})

export class CurrencyTreeviewPipe implements PipeTransform {
  transform(currencies: Currency[]): TreeviewItem[] {
    const treeItems: TreeviewItem[] = [];

    if (_.isNil(currencies)) {
      return undefined;
    }

    currencies.forEach(currency => {
      const item = new TreeviewItem({ text: currency.CurrencyName, value: currency.CurrencyId });
      treeItems.push(item);
    });

    return treeItems;
  }
}

