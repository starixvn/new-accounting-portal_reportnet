import { VirtualRateModel } from './../common/model';
import { Component, OnInit, Input } from '@angular/core';
import * as _ from 'lodash';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-virtual-rate-grid',
  templateUrl: './virtual-rate-grid.component.html',
  styleUrls: ['./virtual-rate-grid.component.scss']
})
export class VirtualRateGridComponent implements OnInit {
  @Input() title: string;
  @Input() data: VirtualRateModel[];

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  get hasData(): boolean {
    return !_.isNil(this.data) && this.data.length > 0;
  }
}
