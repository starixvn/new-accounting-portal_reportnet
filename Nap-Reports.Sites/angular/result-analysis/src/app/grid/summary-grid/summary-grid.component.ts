import { Component, Input } from '@angular/core';
import { ResultAnalysisSummaryModel } from '../grid.model';

@Component({
  selector: 'app-summary-grid',
  templateUrl: './summary-grid.component.html',
  styleUrls: ['./summary-grid.component.scss']
})
export class SummaryGridComponent {
  @Input() data!: ResultAnalysisSummaryModel;
  @Input() isShowTGResult: boolean = true;
}
