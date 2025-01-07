import { SummaryStatistic, BetTypeStatistic, EventStatistic } from '../summary-performance/summary-performance.model';

interface SportStatistic extends SummaryStatistic {
    SportName: string;
}

interface BetCountStatistic {
    EventType: string;
    Turnover: number;
}

export interface SportsbookStatistic {
    SportStatistics: SportStatistic[];
    SummaryStatistic: SummaryStatistic;
}
