export interface AssociatedAccountGridModel {
    custId: number;
    custName: string;
}

export interface AssociatedAccountItem {
    Username: string;
    RoleName: string;
    WinLoss: number;
    Turnover: number;
    BetCount: number;
    Category: string;
}

export interface AssociatedAccountData {
    Entities: AssociatedAccountItem[];
    Total: AssociatedAccountItem;
}
