import { Component, Input, OnChanges } from '@angular/core';
import { DetailedResultAnalysisModel } from '../grid.model';
import { DomSanitizer } from '@angular/platform-browser';
import { PaginationService as NgxPaginationService, PaginatePipe } from 'ngx-pagination';
import { PaginationInstance, PaginationService } from 'nap-components/pagination';
import * as _ from 'lodash';

@Component({
  selector: 'app-item-grid',
  templateUrl: './item-grid.component.html',
  styleUrls: ['./item-grid.component.scss']
})
export class ItemGridComponent implements OnChanges {
  @Input() data: DetailedResultAnalysisModel[] = [];
  displayedData: DetailedResultAnalysisModel[] = [];
  paginationInstance!: PaginationInstance;
  paginatePipe!: PaginatePipe;

  constructor(private readonly sanitizer: DomSanitizer,
    private readonly paginationService: PaginationService,
    private readonly ngxPaginationService: NgxPaginationService) {
      this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
    }

  ngOnChanges(): void {
    this.paginationInstance = this.paginationService.createPaginationInstance(this.data.length);
    this.onPageChange();
  }

  onPageChange() {
    const displayedData = this.paginatePipe.transform(this.data, this.paginationInstance);
    this.displayedData = _.cloneDeep(displayedData);
}

  formatMatch(match: string, fav: string) {
    if (match === null) return '';
    if (fav === null) return match;
    const teams = match.split(' -vs- ');
    if (fav.toLowerCase() === 'home') {
      return this.sanitizer.bypassSecurityTrustHtml(`<span style="color: red;">${teams[0]}</span> -vs- ${teams[1]}`);
    }
    return this.sanitizer.bypassSecurityTrustHtml(`${teams[0]} -vs- <span style="color: red;">${teams[1]}</span>`);
  }
}
