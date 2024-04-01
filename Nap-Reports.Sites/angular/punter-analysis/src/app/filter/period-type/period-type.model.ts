import { NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { PeriodType } from '../filter.model.main';

export interface PeriodTypeEventModel {
    periodType: PeriodType;
    fromDate: NgbDateStruct;
    toDate: NgbDateStruct;
}
