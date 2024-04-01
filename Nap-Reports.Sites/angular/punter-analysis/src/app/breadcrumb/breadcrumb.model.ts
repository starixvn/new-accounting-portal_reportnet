export interface BreadcrumbFilter {
    getFilterColumns(): string[];
    filterColumns(selectedColumns: string[]);
}

export function hasBreadcrumbFilter(component: any): component is BreadcrumbFilter {
    return (component as BreadcrumbFilter).getFilterColumns !== undefined;
}
