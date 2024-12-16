import { Depth } from './filter.model.depth';

export interface PerformanceFilterModel {
    depth: Depth;
    userName: string;
    isUserNameRequired: boolean;
}
