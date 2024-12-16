export enum GridType {
    Product,
    Bettype
}

export interface Grid {
    hasExcel: boolean;
    title: string;
    query(): void;
    exportExcel(): void;
}
