import { ItemModel } from '../../../app/common/model';

export interface MatchDataItemModel extends ItemModel {
  Match: string;
  ActiveMembers: string;
}

export interface MatchDataModel {
  Entities: MatchDataItemModel[];
  Total: MatchDataItemModel;
}
