export enum PerformanceType {
    Main,
    Product
}

export interface Performance {
    title: string;
    query(): void;
}
