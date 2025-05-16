export interface FilterModel {
    fromDate: string;
    toDate: string;
    sportId?: number;
    noDraw: boolean;
    sportName: string;
    breadCrumb: string[];
    liveStatus?: number;
    matchCount?: number;
    filterType: string;
  }
