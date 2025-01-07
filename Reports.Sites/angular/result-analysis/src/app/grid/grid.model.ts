export interface BaseDetailedResultAnalysisModel {
    Date: string;
    League: string;
    Match: string;
    HDP: string;
    OU: string;
    Fav: string;
    HtScoreHome: string;
    HtScoreAway: string;
    FtScoreHome: string;
    FtScoreAway: string;
    ResultHDP: string;
    ResultOU: string;
    Result1x2: string;
}
export interface BaseResultAnalysisSummaryModel {
    TotalLeague: number;
    TotalMatch: number;
    FavHdpResult: number;
    UnderdogHdpResult: number;
    DrawHdpResult: number;
    OverOuResult: number;
    UnderOuResult: number;
    DrawOuResult: number;
    Fav1x2Result: number;
    Underdog1x2Result: number;
    Draw1x2Result: number;
}

export interface DetailedResultAnalysisModel extends BaseDetailedResultAnalysisModel {
    ResultTG: string;
}

export interface ResultAnalysisSummaryModel extends BaseResultAnalysisSummaryModel{
    TgResult01: number;
    TgResult23: number;
    TgResult46: number;
    TgResult7: number;
}

export interface DetailedResultAnalysisOfQuarterModel extends BaseDetailedResultAnalysisModel {
    Q1Home: string;
    Q1Away: string;
    Q2Home: string;
    Q2Away: string;
    Q3Home: string;
    Q3Away: string;
    Q4Home: string;
    Q4Away: string;
    OtHome: string;
    OtAway: string;
    ShtScoreHome: string;
    ShtScoreAway: string;
    HtWM: string;
    HfWM: string;
}

export interface ResultAnalysisModel {
    Items: DetailedResultAnalysisModel[];
    Summary: ResultAnalysisSummaryModel;
}
