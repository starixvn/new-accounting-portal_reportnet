import { Component, ComponentFactory, ComponentFactoryResolver, ComponentRef, Input, OnChanges, OnDestroy, OnInit, Renderer2,
  ViewChild, ViewContainerRef } from '@angular/core';
import { GridService } from './grid.service';
import { FilterModel } from '../filter/filter.model';
import * as _ from 'lodash';
import { Grid, GridType } from './grid.model';
import { MatchDetailGridComponent } from './match-detail-grid/match-detail-grid.component';
import { SportGridComponent } from './sport-grid/sport-grid.component';
import { EventStatusGridComponent } from './event-status-grid/event-status-grid.component';
import { BettypeGridComponent } from './bettype-grid/bettype-grid.component';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  entryComponents: [
    MatchDetailGridComponent,
    SportGridComponent,
    EventStatusGridComponent,
    BettypeGridComponent
  ]
})
export class GridComponent implements OnInit, OnChanges, OnDestroy {
  @Input() filterModel: FilterModel;
  @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;

  gridType = GridType.Sport;
  breadcrumbItems: string[];
  private components: ComponentRef<Grid>[] = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private renderer: Renderer2,
    private service: GridService
  ) { }

  ngOnInit(): void {
    this.service.generate = (gridType: GridType,) => this.generate(gridType);
  }

  ngOnDestroy(): void {
    this.container.clear();
  }

  ngOnChanges(): void {
    this.breadcrumbItems = [];
    while (this.components.length) {
      const component = this.components.pop();
      component.destroy();
    }
    this.gridType = GridType.Sport;
    this.service.filterModel = this.filterModel;
    if (this.filterModel !== undefined)
    {
      this.service.orginalSportBettype = this.filterModel.sportBettype;
      this.service.cachedSportBettype = this.filterModel.sportBettype;
      this.service.cachedLiveIndicator = this.filterModel.liveIndicator;
    }
    this.createGridComponent();
  }

  private generate(gridType: GridType): void {
    this.filterModel.isPageChanged = false;
    this.gridType = gridType;
    this.createGridComponent();
  }

  private getComponentFactory(): ComponentFactory<Grid> {
    switch (this.gridType) {
        case GridType.Sport:
            return this.componentFactoryResolver.resolveComponentFactory(SportGridComponent);
        case GridType.Event:
            return this.componentFactoryResolver.resolveComponentFactory(EventStatusGridComponent);
        case GridType.BetType:
            return this.componentFactoryResolver.resolveComponentFactory(BettypeGridComponent);
        case GridType.MatchDetail:
            return this.componentFactoryResolver.resolveComponentFactory(MatchDetailGridComponent);
        default:
            throw new Error('Not implemented yet');
    }
  }

  private createGridComponent(): void {
    const factory = this.getComponentFactory();
    if (this.container !== undefined)
    {
      const component = this.container.createComponent(factory);
      this.components.push(component);

      const grid = component.instance;
      this.service.activeGrid = grid;
      grid.query().subscribe({
        complete: () => this.showGridAndBreadcrumb()
      });
    }
  }

  private showGridAndBreadcrumb(): void {
    this.updateBreadcrumb();
    this.showOnlyLastGrid();
  }

  private updateBreadcrumb(): void {
    this.breadcrumbItems = [];
    this.components.forEach((component, index) => {
      const componentTitle = component.instance.title.toUpperCase();
      this.breadcrumbItems.push(componentTitle);
    });
    this.service.filterModel.breadCrumb = this.breadcrumbItems;
  }

  private showOnlyLastGrid(): void {
    const length = this.components.length;
    const currentComponent = this.components[length - 1];
    this.renderer.removeAttribute(currentComponent.location.nativeElement, 'hidden');

    if (length > 1) {
      const lastComponent = this.components[length - 2];
      this.renderer.setAttribute(lastComponent.location.nativeElement, 'hidden', 'hidden');
    }
  }

  private destroyComponentsFrom(index: number): void {
    while (this.components.length - 1 > index) {
      const component = this.components.pop();
      component.destroy();
    }

    this.service.activeGrid = this.components[this.components.length - 1].instance;
    this.showGridAndBreadcrumb();
  }

  onBreadcrumbItemClick(event: number): void {
    this.destroyComponentsFrom(event);
    this.showGridAndBreadcrumb();
  }

  exportExcel(): void {
    if (this.components.length === 0) {
        return;
    }
    const component = this.components[this.components.length - 1];
    const grid = component.instance;
    grid.exportExcel();
}
}
