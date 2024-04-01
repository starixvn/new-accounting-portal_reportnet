import { ItemModel } from '../../../app/common/model';

export interface UsernameDataItemModel extends ItemModel {
  Username: string;
  ActiveMembers: string;
}

export interface UsernameDataModel {
  Entities: UsernameDataItemModel[];
  Total: UsernameDataItemModel;
}
