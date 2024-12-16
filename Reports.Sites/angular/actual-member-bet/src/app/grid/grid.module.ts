import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridComponent } from './grid.component';
import { GridService } from './grid.service';
import { BettypeGridService } from './bettype-grid/bettype-grid.service';
import { ProductGridService } from './product-grid/product-grid.service';
import { BettypeGridComponent } from './bettype-grid/bettype-grid.component';
import { ProductGridComponent } from './product-grid/product-grid.component';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        NumberFormatModule,
        BreadcrumbModule
    ],
    declarations: [
        GridComponent,
        ProductGridComponent,
        BettypeGridComponent
    ],
    exports: [
        GridComponent
    ],
    providers: [
        GridService,
        ProductGridService,
        BettypeGridService
    ]
})

export class GridModule { }
