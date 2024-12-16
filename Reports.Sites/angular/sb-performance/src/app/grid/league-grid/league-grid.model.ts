import { ItemModel } from '../../../app/common/model';

export interface LeagueDataItemModel extends ItemModel {
    League: string;
    ActiveMembers: string;
  }

export interface LeagueDataModel {
    Entities: LeagueDataItemModel[];
    Total: LeagueDataItemModel;
}
