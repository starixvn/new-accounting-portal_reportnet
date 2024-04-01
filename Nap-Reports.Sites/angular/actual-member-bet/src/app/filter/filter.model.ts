
export interface Currency {
    CurrencyId: number;
    CurrencyName: string;
  }

  export interface Odds {
    OddsType: string;
    OddsTypeName: string;
  }

  export interface Site {
    SiteType: number;
    SiteName: string;
  }

  export interface FilterModel {
      fromDate: string;
      toDate: string;
      currencyId: number;
      oddsType: number;
      siteType: number;
  }
