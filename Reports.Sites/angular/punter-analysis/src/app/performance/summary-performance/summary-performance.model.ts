export interface SummaryStatistic {
    BetCount: number;
    Turnover: number;
    WinLoss: number;
}

export interface BetTypeStatistic extends SummaryStatistic {
    BetTypeName: string;
}

export interface EventStatistic extends SummaryStatistic {
    EventType: string;
}

interface MonthStatistic extends SummaryStatistic {
    Date: string;
}

interface ProductTurnoverStatistic {
    ProductName: string;
    Turnover: number;
}

interface SportStatistic extends SummaryStatistic {
    SportName: string;
}

export interface PerformanceStatistic {
    BetTypeStatistics: BetTypeStatistic[];
    EventStatistics: EventStatistic[];
    MonthStatistics: MonthStatistic[];
    ProductTurnoverStatistics: ProductTurnoverStatistic[];
    SportStatistics: SportStatistic[];
    SummaryStatistic: SummaryStatistic;
}
