export interface FilterItemModel {
  Id: number;
  Name: string;
}

export interface LeagueModel {
  Id: string;
  Name: string;
}

export interface SiteModel {
  SiteGroup: string;
  SiteId: number;
  SiteName: string;
  SiteLabel: string;
}


export interface FilterModel {
  fromDate: string;
  toDate: string;
  reason: number[];
  sport: number[];
  bettype: number[];
  licensee: number[];
  member: string;
  isTest: boolean;
  leagueId: string;
  pageIndex: number;
  pageSize: number;
  isPageChanged: boolean;
  currencyId: string;
}

export enum PeriodType {
  Daily = 1,
  Monthly = 2,
  Annually = 3
}

