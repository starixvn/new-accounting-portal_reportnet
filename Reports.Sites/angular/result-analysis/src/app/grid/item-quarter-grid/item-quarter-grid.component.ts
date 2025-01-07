import { Component, Input, OnChanges } from '@angular/core';
import { DetailedResultAnalysisModel } from '../grid.model';
import { DomSanitizer } from '@angular/platform-browser';
import { PaginationService as NgxPaginationService, PaginatePipe } from 'ngx-pagination';
import { PaginationInstance, PaginationService } from 'nap-components/pagination';
import * as _ from 'lodash';

@Component({
  selector: 'app-item-quarter-grid',
  templateUrl: './item-quarter-grid.component.html',
  styleUrls: ['./item-quarter-grid.component.scss']
})
export class ItemQuarterGridComponent implements OnChanges {
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

  getMatch(match: string, fav: string, isHome: boolean) {
    if (match === null) return '';
    if (fav === null) return '';
    const teams = match.split(' -vs- ');
    if (isHome) {
      return fav.toLowerCase() === 'home' ?
        this.sanitizer.bypassSecurityTrustHtml(`<span style="color: red;">${teams[0]}</span>`) : teams[0];
    } else {
      return fav.toLowerCase() === 'away' ?
        this.sanitizer.bypassSecurityTrustHtml(`<span style="color: red;">${teams[1]}</span>`) : teams[1];
    }
  }

}
