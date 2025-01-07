export interface SummaryItem {
    CustId: number;
    Username: string;
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
    Category: string;
}

export interface SummaryData {
    Entities: SummaryItem[];
    Total: SummaryItem;
}
