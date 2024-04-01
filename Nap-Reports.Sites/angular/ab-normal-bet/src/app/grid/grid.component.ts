import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ViewContainerRef,
  ComponentFactoryResolver,
  Injector,
  SimpleChanges,
  OnChanges,
  ComponentFactory,
  OnDestroy
} from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { MemberGridComponent } from './../member-grid/member-grid.component';
import { LicenseeGridComponent } from '../licensee-grid/licensee-grid.component';
import { Grid } from './../common/model';
import { GridFilterService } from './../common/grid-filter.service';
import { QueryModelService } from './../common/query-model.service';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  entryComponents: [
    MemberGridComponent,
    LicenseeGridComponent,
  ],
  providers: [
    GridFilterService,
    QueryModelService]
})
export class GridComponent implements OnChanges, OnDestroy {

  breadcrumbItems: string[];
  @Input() filterModel: FilterModel;
  @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private gridFilterService: GridFilterService
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    this.clear();

    if (this.filterModel) {
      this.gridFilterService.filterModel = this.filterModel;
      this.createGrid();
    }
  }

  ngOnDestroy() {
    this.clear();
  }

  private createGrid() {
    const factory = this.getComponentFactory();
    const component = this.container.createComponent(factory);
    const grid = component.instance;
    grid.query();
  }

  private getComponentFactory(): ComponentFactory<any> {
    if (this.filterModel.member !== '') {
      return this.componentFactoryResolver.resolveComponentFactory(MemberGridComponent);
    } else {
      return this.componentFactoryResolver.resolveComponentFactory(LicenseeGridComponent);
    }
  }

  private clear() {
    this.container.clear();
  }
}
