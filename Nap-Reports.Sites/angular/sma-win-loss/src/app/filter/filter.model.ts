export interface FilterModel {
  year: number;
  custLevel: number;
  username: string;
  isDownline: boolean;
  currencies: string;
  isAppliedVR: number;
  sports: string;
  leagueIds: string;
  divisions: string;
  accountStatus: string;
  vrSettings: string;
  pageSize: number;
  pageIndex: number;
  isPageChanged: boolean;
  liveStatus: string;
  betFromIds: string;
  permissionProfileId: number
}

export class SelectItem {
  name: string;
  value: number;

  constructor(name: string, value: number) {
    this.name = name;
    this.value = value;
  }

  public get Name() {
    return this.name;
  }

  public get Value() {
    return this.value;
  }
}

export interface Currency {
  CurrencyId: number;
  CurrencyName: string;
}

export interface Site {
  SiteGroup: string;
  SiteId: number;
  SiteName: string;
  SiteLabel: string;
}

export interface Sport {
  SportGroup: string;
  SportIds: string;
}

export interface LeagueGroup {
  GroupName: string;
  LeagueIds: string;
  ChildItems: LeagueGroupItem[];
}

export interface CommonItem {
  Value: string;
  Text: string;
}

export interface BetFrom {
  Platform: string;
  BetFromId: string;
}

export interface LeagueGroupItem {
  Label: string;
  GroupName: string;
  LeagueIds: string;
}

export class LeagueGroupModel {
  constructor(private readonly _numberItems: number, private readonly _leagueGroups: LeagueGroup[]) {
    this.NumberItems = _numberItems;
    this.LeagueGroups = _leagueGroups;
  }
  NumberItems: number;
  LeagueGroups: LeagueGroup[]
}


