export interface FilterModel {
  fromDate: string;
  toDate: string;
  timeRange: number;
  margin: number;
  topRows: number;
  isShowFullColumn: boolean;
  topBy: string;
  excludeDrawTickets: boolean;
  excludeParlay: boolean;
}

export class SelectItem {
  name: string;
  value: string;

  constructor(name: string, value: string) {
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
