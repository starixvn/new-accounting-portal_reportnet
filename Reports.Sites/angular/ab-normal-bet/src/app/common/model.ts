import { Observable } from 'rxjs';

export interface Grid {
    hasExcel: boolean;
    hasData: boolean;
    query(): void;
    exportExcel(title: string): void;
}

export interface MemberItemModel {
  RefNo: string;
  Sport: string;
  League: string;
  Match: string;
  Reason: string;
  Odds: number;
  Currency: number;
  Stake: number;
  HDP: number;
  Score: string;
  TransTime: string;
  IP: string;
  Location: string;
}

export interface LicenseeItemModel {
  Date: string;
  Licensee: string;
  Currency: string;
  Sport: string;
  League: string;
  Match: string;
  Reason: string;
  Live: string;
  BetCount: number;
  BetType: string;
  Choice: string;
  BettingScore: string;
  HDPOU: number;
  HTScore: string;
  FinalScore: string;
  MemberTO: number;
  MemberTORM: number;
  AfterPT: number;
  AfterPTRM: number;
}

export interface QueryModel {
    fromDate: string;
    toDate: string;
    reason: string;
    bettype: string;
    sport: string;
    licensee: string;
    isTest: boolean;
    member: string;
    leagueId: string;
    pageSize: number;
    pageIndex: number;
    currencyId: string;
}

export interface ExcelQueryModel extends QueryModel {
    title: string;
}

interface BalanceUpRange {
    balanceUpDay: string;
    moveBetsDay: string;
}

export interface BalanceUpModel {
    today: string;
    balanceUpRange: BalanceUpRange;
}

export interface MemberModel {
  Members: MemberItemModel[];
  TotalItems: number;
}

export interface LicenseeModel {
  Licensees: LicenseeItemModel[];
  TotalItems: number;
}

