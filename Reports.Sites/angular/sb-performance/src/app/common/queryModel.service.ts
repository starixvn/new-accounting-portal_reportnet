import { FilterModel } from "../filter/filter.model";
import { QueryModel } from "./model";
import { Injectable } from "@angular/core";

@Injectable()
export class QueryModelService {
  createQueryModel(
    filterModel: FilterModel,
    isShowSite: boolean,
    isShowSport: boolean,
    isShowLeague: boolean,
    isShowCust: boolean,
  ): QueryModel {
    const queryModel: QueryModel = {
      fromDate: filterModel.fromDate,
      toDate: filterModel.toDate,
      timeRange: filterModel.timeRange,
      isShowSite,
      isShowSport,
      isShowLeague,
      isShowCust,
      margin: filterModel.margin,
      top: filterModel.topRows,
      isShowFullColumn: filterModel.isShowFullColumn,
      topBy: filterModel.topBy,
      excludeDrawTickets: filterModel.excludeDrawTickets,
      excludeParlay: filterModel.excludeParlay
    };

    return queryModel;
  }
}
