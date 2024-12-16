import { ItemModel } from '../../../app/common/model';

export interface SiteDataItemModel extends ItemModel {
  Site: string;
  ActiveMembers: string;
}

export interface SiteDataModel {
  Entities: SiteDataItemModel[];
  Total: SiteDataItemModel;
}
