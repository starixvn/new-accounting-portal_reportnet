import { Observable } from "rxjs";

export interface Grid {
    title: string;
    query(): Observable<any>;
    exportExcel(): void;
}

export enum GridType {
    Licensee,
    Sport,
    Event
}

export interface QueryModel {
  licenseeName: string;
  eventType: string;
  drillLevel: number;
}

export interface ExchangeItemModel {
    Name: string;
    MarketType: string;
    MemberCount: number;
    BetCount: number;
    TurnOver: number;
    MemberWinLoss: number;
    MemberCommission: number;
    MemberTotal: number;
    LicenseeWinLoss: number;
    LicenseeCommission: number;
    LicenseeTotal: number;
    CompanyWinLoss: number;
    CompanyCommission: number;
    PayToSupplier: number;
    CompanyNetIncome: number;
  }

  export interface ExchangeLicenseeItemModel {
    Entities: ExchangeItemModel[];
    Total: ExchangeItemModel;
  }

  export interface ExchangeLicenseeModel {
    ExchangeLicensees: ExchangeLicenseeModel[];
    GrandTotal: ExchangeItemModel;
    MarketTypeTotal: ExchangeItemModel[];
    TotalItems: number;
  }

  export interface ExchangeModel {
    Entities: ExchangeItemModel[];
    Total: ExchangeItemModel;
  }
