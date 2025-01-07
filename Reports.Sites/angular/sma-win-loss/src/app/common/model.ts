export interface QueryModel {
  year: number;
  custLevel: number;
  username: string;
  isDownline: boolean;
  currencies: string;
  isAppliedVR: boolean;
  sports: string;
  leagueIds: string;
  divisions: string;
  accountStatus: string;
  vrSettings: string;
  pageSize: number;
  pageIndex: number;
  permissionProfileId: number;
  liveStatus: string;
  betFromIds: string;
}

export interface MonthModel {
  Month: string;
  Value: number;
}

export interface UsernameModel {
  CustId: number;
  Username: string;
  Currency: string;
  Name: string;
  VirtualRate: string;
  Months: MonthModel[];
}

export interface WinLossModel {
  UsernameWinLosses: UsernameModel[];
  Total: UsernameModel;
  TotalItems: number;
}

export interface VirtualRateModel {
  Sport: string;
  Bettype: string;
  IsLive: string;
  VirtualMultiplier: number;
}

export interface ExcelQueryModel extends QueryModel {
  title: string;
}
