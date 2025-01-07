import { Component, Input, OnChanges } from '@angular/core';
import { TreeviewItem } from 'ngx-treeview';
import * as _ from 'lodash';
import { BreadcrumbConfig } from './breadcrumb-config';

@Component({
    selector: 'nap-breadcrumb',
    templateUrl: './breadcrumb.component.html',
    styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent implements OnChanges {
    @Input() config: BreadcrumbConfig;
    @Input() items: string[];
    filterItems: TreeviewItem[];
    isShowFilter = false;

    ngOnChanges() {
        this.isShowFilter = false;
        this.filterItems = undefined;
    }

    get hasExcel(): boolean {
        return !_.isNil(this.config) && !_.isNil(this.config.exportExcel);
    }

    exportExcel() {
        if (this.hasExcel) {
            this.config.exportExcel();
        }
    }

    goBack(index: number) {
        if (!_.isNil(this.config) && !_.isNil(this.config.goBack)) {
            this.config.goBack(index);
        }
    }

    get hasFilter(): boolean {
        return !_.isNil(this.config) && !_.isNil(this.config.filterColumns);
    }

    toggleFilter() {
        this.isShowFilter = !this.isShowFilter;
        if (!this.filterItems) {
            this.filterItems = this.config.filterColumns.map(column => new TreeviewItem({ text: column, value: column }));
        }
    }

    onFilterSelectedChange(columns: string[]) {
        if (this.config.filterCallback) {
            this.config.filterCallback(columns);
        }
    }
}
