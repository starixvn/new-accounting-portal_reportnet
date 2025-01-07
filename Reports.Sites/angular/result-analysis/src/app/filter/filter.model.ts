export interface LeagueModel {
    GroupId: number;
    GroupName: string;
    LeagueId: number;
    LeagueName: string;
    LengthOfLeagueName: number;
}

export interface FilterModel {
    fromDate: string;
    toDate: string;
    sportId: number;
    leagueGroupIds: string;
    leagueName?: string;
  }
