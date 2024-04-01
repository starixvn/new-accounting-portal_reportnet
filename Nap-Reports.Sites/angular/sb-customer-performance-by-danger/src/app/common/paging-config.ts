import { Injectable } from "@angular/core";
import { PaginationConfig } from "nap-components/pagination";

@Injectable()
export class CustomPaginationConfig extends PaginationConfig {
    pageSizes = [50, 100, 200, 500, 1000];
    itemsPerPage = 1000;
}
