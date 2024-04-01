import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TreeviewModule } from 'ngx-treeview';
import { BreadcrumbComponent } from './breadcrumb.component';

@NgModule({
    imports: [
        CommonModule,
        TreeviewModule.forRoot()
    ],
    declarations: [
        BreadcrumbComponent
    ],
    exports: [
        BreadcrumbComponent
    ]
})
export class BreadcrumbModule { }
