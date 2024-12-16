import {
  Component,
  ViewChild,
  ViewContainerRef,
  ComponentFactoryResolver,
  ComponentFactory,
  Input,
  OnDestroy,
  OnChanges,
  ReflectiveInjector,
  Injector,
  SimpleChanges
} from '@angular/core';
import { UpdatedUsernameComponent } from './../updated-username/updated-username.component';
import { UpdatedCreditComponent } from './../updated-credit/updated-credit.component';
import { FilterModel } from './../filter/filter.model';
import { QueryModelService } from './../common/query-model.service';
import { BreadcrumbService } from './../common/breadcrumb.service';
import { UpdatedUsernameService } from './../updated-username/updated-username.service';
import { UpdatedCreditService } from './../updated-credit/updated-credit.service';


@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: [
    './grid.component.scss'
  ],
  entryComponents: [
    UpdatedCreditComponent,
    UpdatedUsernameComponent,
  ],
  providers: [
    UpdatedCreditService,
    UpdatedUsernameService,
    QueryModelService,
    BreadcrumbService
  ]
})
export class GridComponent implements OnChanges, OnDestroy {
  breadcrumbItems: string[];
  @Input() filterModel: FilterModel;
  @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private injector: Injector
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    this.clear();

    if (this.filterModel) {
      this.createComponent();
    }
  }

  ngOnDestroy() {
    this.clear();
  }

  private createComponent() {
    const injector = ReflectiveInjector.resolveAndCreate([
      { provide: FilterModel, useValue: this.filterModel }
    ], this.injector);

    const factory = this.getComponentFactory();
    const component = this.container.createComponent(factory, 0, injector);
  }

  private getComponentFactory(): ComponentFactory<any> {
    switch (this.filterModel.reportBy) {
      case 'Updated Credit':
        return this.componentFactoryResolver.resolveComponentFactory(UpdatedCreditComponent);
      case 'Updated Username':
        return this.componentFactoryResolver.resolveComponentFactory(UpdatedUsernameComponent);
      default:
        throw new Error('Not implemented yet');
    }
  }

  private clear() {
    this.container.clear();
  }
}
