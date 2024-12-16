import { Component, ComponentFactory, ComponentFactoryResolver, ComponentRef, Input, OnChanges, OnDestroy, OnInit, Renderer2, ViewChild, ViewContainerRef } from '@angular/core';
import { Grid, GridType, QueryModel } from './grid.model';
import { GridService } from './grid.service';
import { LicenseeComponent } from './licensee/licensee.component';
import { SportComponent } from './sport/sport.component';
import { EventComponent } from './event/event.component';
import * as _ from 'lodash';
import { FilterModel } from '../filter/filter.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { DateHelper } from 'nap-components/date';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  entryComponents: [
    SportComponent,
    LicenseeComponent,
    EventComponent
  ]
})
export class GridDrillDownComponent implements OnInit, OnChanges, OnDestroy {
  @Input() filterModel: FilterModel;
  @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;
  
  gridType = GridType.Licensee;
  breadcrumbItems: string[];
  private components: ComponentRef<Grid>[] = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private renderer: Renderer2,
    private service: GridService
  ) { }

  ngOnInit() {
    this.service.generate = (gridType: GridType, queryModel: QueryModel) => this.generate(gridType, queryModel);
  }

  ngOnDestroy() {
    this.container.clear();
  }

  ngOnChanges() {
    if (this.filterModel) {
      this.breadcrumbItems = [];
      while (this.components.length) {
        const component = this.components.pop();
        component.destroy();
      }
      this.gridType = GridType.Licensee;
      this.service.filterModel = this.filterModel;
      this.service.queryModel = this.filterModel;
      this.createGridComponent();
    }
  }

  exportExcel() {
    if (this.components.length === 0) {
      return;
    }

    const component = this.components[this.components.length - 1];
    const grid = component.instance;
    grid.exportExcel();
  }

  private generate(gridType: GridType, model: QueryModel) {
    this.service.queryModel = _.assign(this.service.filterModel, model);
    this.gridType = gridType;
    this.createGridComponent();
  }

  private getComponentFactory(): ComponentFactory<Grid> {
    switch (this.gridType) {
        case GridType.Licensee:
            return this.componentFactoryResolver.resolveComponentFactory(LicenseeComponent);
        case GridType.Sport:
            return this.componentFactoryResolver.resolveComponentFactory(SportComponent);
        case GridType.Event:
            return this.componentFactoryResolver.resolveComponentFactory(EventComponent);
        default:
            throw new Error('Not implemented yet');
    }
  }

  private createGridComponent() {
    const factory = this.getComponentFactory();
    const component = this.container.createComponent(factory);
    this.components.push(component);

    const grid = component.instance;
    this.service.activeGrid = grid;
    grid.query().subscribe({
      complete: () => this.showGridAndBreadcrumb()
    });
  }

  private updateBreadcrumb() {
    this.breadcrumbItems = [];
    this.components.forEach((component) => {
      const componentTitle = component.instance.title;
      this.breadcrumbItems.push(componentTitle);
    });
  }

  private showOnlyLastGrid() {
    const length = this.components.length;
    const currentComponent = this.components[length - 1];
    this.renderer.removeAttribute(currentComponent.location.nativeElement, 'hidden');

    if (length > 1) {
      const lastComponent = this.components[length - 2];
      this.renderer.setAttribute(lastComponent.location.nativeElement, 'hidden', 'hidden');
    }
  }

  private showGridAndBreadcrumb() {
    this.updateBreadcrumb();
    this.showOnlyLastGrid();
  }

  private destroyComponentsFrom(index: number) {
    while (this.components.length - 1 > index) {
      const component = this.components.pop();
      component.destroy();
    }

    this.service.activeGrid = this.components[this.components.length - 1].instance;
    this.showGridAndBreadcrumb();
  }

  onBreadcrumbItemClick(event: number) {
    this.destroyComponentsFrom(event);
  }

}
