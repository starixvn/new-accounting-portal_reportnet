  export enum TurnoverType {
    Normal,
    NoDraw
  }

  export enum DateType {
    WinlossDate = 'Winlostdate',
    TransDate = 'Transdate'
  }

  export interface ReportItemModel {
    SportId: number;
    Sport: string;
    Date: Date;
    LeagueId: number;
    LeagueName: string;
    MatchName: string;
    BetType: number;
    BetTypeName: string;
    MatchId: number;
    MatchCount: number;
    BetCount: number;
    MemberWinLoss: number;
    MemberTurnover: number;
    ActualTurnover: number;
    HouseIncome: number;
    HouseTurnover: number;
    AmountDueToDST: number;
    Margin: number;
  }

  export interface ReportModel {
    Items: ReportItemModel[];
    Total: ReportItemModel;
  }
