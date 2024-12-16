export enum TimePeriod {
    Today,
    Yesterday,
    CurrentWeek,
    LastWeek,
    CurrentMonth,
    LastMonth,
    SinceLastMonth,
    SpecificDate,
    LastThreeMonths
}

export enum TicketStatus {
  NonLive,
  Live,
  All
}
export interface FilterItemModel {
    Id: number;
    Name: string;
}

export interface LeagueModel {
    Id: string;
    Name: string;
  }
export interface FilterGroupItemModel {
  GroupId: number;
  GroupName: string;
  ChildItems: FilterItemModel[];
}

export interface FilterModel {
    startDate: string;
    endDate: string;
    sportBettype: string;
    leagueGroupIDs: string;
    sites: string;
    leagues: string;
    pageIndex: number;
    pageSize: number;
    isPageChanged: boolean;
    globalShowTime: string;
    liveIndicator: number;
    breadCrumb: string[];
    server: string;
    isShowFullColumn: boolean;
    exchangeBy: boolean,
    filterType: number
  }

  export interface SiteModel {
    SiteGroup: string;
    SiteId: number;
    SiteName: string;
    SiteLabel: string;
}

export enum ExchageRateValues {
  SystemRate,
  OandaRate
}
