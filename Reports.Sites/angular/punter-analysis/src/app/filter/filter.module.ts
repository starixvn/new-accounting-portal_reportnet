import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TreeviewModule } from 'ngx-treeview';

import { SelectModule } from 'nap-components/select';
import { LegendModule } from 'nap-components/legend';
import { DateModule } from 'nap-components/date';
import { InputIntegerDirective } from './input-integer.directive';
import { DropdownTypeaheadModule } from 'nap-components/dropdown-typeahead';
import { DropdownSelectModule } from 'nap-components/dropdown-select';

import { PeriodTypeModule } from './period-type/period-type.module';
import { FilterComponent } from './filter.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        SelectModule,
        TreeviewModule.forRoot(),
        LegendModule,
        DateModule,
        DropdownTypeaheadModule,
        DropdownSelectModule,
        PeriodTypeModule
    ],
    declarations: [
        InputIntegerDirective,
        FilterComponent
    ],
    exports: [
        FilterComponent
    ]
})
export class FilterModule { }
