import { ItemModel } from '../../../app/common/model';

export interface EventDataItemModel extends ItemModel {
  Event: string;
  NoMatches: number;
  NoBetTypes: number;
  ActiveMembers: string;
}

export interface EventDataModel {
  Entities: EventDataItemModel[];
  Total: EventDataItemModel;
}