export interface BreadcrumbConfig {
    filterColumns?: string[];
    filterCallback?(selectedColumns: string[]): void;
    goBack?(index: number): void;
    exportExcel?(): void;
}
