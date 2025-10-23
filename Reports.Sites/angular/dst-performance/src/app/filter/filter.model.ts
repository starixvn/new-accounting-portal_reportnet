export interface FilterModel {
    fromDate: string;
    toDate: string;
    noDraw: boolean;
    breadCrumb: string[];
    liveStatus: number;
    sportName: string;
    sportId?: number;
    betType?: number;
    leagueId?: number;
    leagueName: string;
    betTypeName: string;
    level: string;
  }
