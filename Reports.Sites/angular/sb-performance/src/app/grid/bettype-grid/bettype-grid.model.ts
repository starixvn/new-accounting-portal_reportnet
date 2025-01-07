import { ItemModel } from '../../../app/common/model';

export interface BetTypeDataItemModel extends ItemModel {
  BetType: string;
  ActiveMembers: string;
}

export interface BetTypeDataModel {
  Entities: BetTypeDataItemModel[];
  Total: BetTypeDataItemModel;
}