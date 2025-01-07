import { FilterModel } from "../filter/filter.model";

export interface CommonTicketModel {
  COTurnover_RM: number;
  BBAmount_RM: number;
  MemberWL_RM: number;
  Comm_RM: number;
  COIncome_RM: number;
  TotalIncome_RM: number;
  ActualTurnover: number;
  AssumedPayout: number;
  HypGGR: number;
  COProfit: number;
  Margin: number;
}
export interface TicketModel extends CommonTicketModel {
    WLDate: Date;
    Username: string;
    Currency: string;
    OrigID: number;
    BuyBackID: number;
    CashOutID: number;
    SportName: string;
    LeagueName: string;
    MatchName: string;
    BetTime: Date;
    CashOutTime: Date;
    BetOdd: string;
    BetTypeName: string;
    BetChoice: string;
    COTurnover: number;
    BBAmount: number;
    MemberWL: number;
  }

export interface SportItemModel extends CommonTicketModel {
    SportType: number;
    SportName: string;
    LeagueName: string;
}

export interface SportModel {
  SportTotal: SportItemModel;
  ChildItems: SportItemModel[];
}

export interface SportLevelModel {
  Sports: SportModel[];
  GrandTotal: SportItemModel;
  TotalItems: number;
}

export interface EventStatusLevelModel extends CommonTicketModel {
    LiveStatus: number;
}

export interface BetTypeLevelModel extends CommonTicketModel {
  BetType: number;
  BetTypeName: string;
  BetCount: number;
  OutCome: string;
  WinningProbabilityQuantile: string;
}

export interface MatchDetailLevelModel extends CommonTicketModel {
  WLDate: Date;
  Username: string;
  Currency: string;
  OrigID: string;
  BuyBackID: string;
  CashOutID: string;
  SportName: string;
  LeagueName: string;
  MatchName: string;
  BetTime: Date;
  CashOutTime: Date;
  BetOdd: string;
  BetTypeName: string;
  BetChoice: string;
  COTurnover: number;
  BBAmount: number;
  MemberWL: number;
  BetScore: string;
  CurrentGoalLine: string;
  CurrentHomeOdds: string;
  CurrentAwayOdds: string;
  CurrentScore: string;
  SiteGroup: string;
}

export interface TicketViewModel<T> {
    TicketList: T[];
    TotalTicket: T;
    Total: number;
  }

export interface ExcelQueryModel {
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
    title: string;
    exchangeBy: boolean;
    filterType: number;
  }

  export enum FilterType {
    SettledDate,
    CashedOutDate
  }

  export interface BetTypeDrillDownQueryModel extends FilterModel {
    betType: number;
    outCome: string;
    title?: string;
    type?: string;
  }
