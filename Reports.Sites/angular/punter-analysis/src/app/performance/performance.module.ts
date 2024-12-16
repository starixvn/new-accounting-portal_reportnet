import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'nap-components/chart';
import { NumberFormatModule } from 'nap-components/number-format';
import { PerformanceComponent } from './performance.component';
import { PerformanceService } from './performance.service';
import { SummaryPerformanceComponent } from './summary-performance/summary-performance.component';
import { SummaryPerformanceService } from './summary-performance/summary-performance.service';
import { ProductPerformanceComponent } from './product-performance/product-performance.component';
import { ProductPerformanceService } from './product-performance/product-performance.service';
import { BreadcrumbModule } from '../breadcrumb/breadcrumb.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ChartsModule,
        NumberFormatModule,
        BreadcrumbModule
    ],
    declarations: [
        PerformanceComponent,
        SummaryPerformanceComponent,
        ProductPerformanceComponent
    ],
    exports: [
        PerformanceComponent
    ],
    providers: [
        PerformanceService,
        SummaryPerformanceService,
        ProductPerformanceService
    ]
})
export class PerformanceModule { }
