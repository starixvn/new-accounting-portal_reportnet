import { ItemModel } from '../../../app/common/model';

export interface SportDataItemModel extends ItemModel {
  Sport: string;
  ActiveMembers: string;
}

export interface SportDataModel {
  Entities: SportDataItemModel[];
  Total: SportDataItemModel;
}
