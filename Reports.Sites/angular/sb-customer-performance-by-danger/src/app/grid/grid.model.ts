export interface CustomerPerformanceByDangerModel {
  UserName: string;
  AgentName: string;
  MasterName: string;
  SuperName: string;
  Division: string;
  Currency: string;
  DangerLevel: string;
  BetCount: number;
  MemberTO: number;
  MemberWL: number;
  HouseTO: number;
  HouseIncome: number;
  AVGStake: number;
  NumberOfAssociatedAccount: number;
  Margin: number;
  GivenCredit: number;
}

export interface SummaryEntity {
  Entities: CustomerPerformanceByDangerModel[];
  Total: CustomerPerformanceByDangerModel;
}

export interface ExcelQueryModel {
  fromDate: string;
  toDate: string;
  divisions: string;
  dangerLevels: string;
  sportBettypes: string;
  leagueName: string;
  leagueGroupIds: string;
  liveStatus: string;
  topUsers: number;
  title: string;
}
