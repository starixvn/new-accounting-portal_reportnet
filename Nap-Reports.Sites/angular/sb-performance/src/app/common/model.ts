export interface QueryModel {
  fromDate: string;
  toDate: string;
  timeRange: number;
  isShowSite: boolean;
  isShowSport: boolean;
  isShowLeague: boolean;
  isShowCust: boolean;
  margin: number;
  top: number;
  isShowFullColumn: boolean;
  topBy: string;
  excludeDrawTickets: boolean;
  excludeParlay: boolean;
}

export interface ItemModel {
  Id: string;
  Name: string;
  BetCount: number;
  RealIncome: number;
  RealTurnover: number;
  RealMargin: number;
}

export interface MainItemModel {
  Entities: ItemModel[];
  Total: ItemModel;
}
