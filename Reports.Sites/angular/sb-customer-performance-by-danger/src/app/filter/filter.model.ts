export interface FilterModel {
  fromDate: string;
  toDate: string;
  divisions: string;
  dangerLevels: string;
  sportBettypes: string;
  leagueName: string;
  leagueGroupIds: string;
  liveStatus: string;
  topUsers: number;
}

export interface Site {
  SiteGroup: string;
  SiteId: number;
  SiteName: string;
  SiteLabel: string;
}

export interface LeagueGroup {
  GroupId: number;
  GroupName: string;
}

export interface Sport {
  SportType: number;
  SportName: string;
  BettypeGroups: BettypeGroupModel[];
}

export interface Status {
  StatusName: string;
  StatusValue: number;
}

export interface BettypeGroupModel
{
  BettypeGroupName: string;
  Bettypes: BettypeModel[];
}

export interface BettypeModel
{
  Bettype: string;
  BettypeName: string;
}
