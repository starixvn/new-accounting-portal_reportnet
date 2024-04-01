import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DateModule } from 'nap-components/date';
import { PeriodTypeComponent } from './period-type.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        DateModule
    ],
    declarations: [
        PeriodTypeComponent
    ],
    exports: [
        PeriodTypeComponent
    ]
})
export class PeriodTypeModule { }
