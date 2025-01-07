import { FilterModel } from "../filter/filter.model";
import { QueryModel, ExcelQueryModel } from "./model";
import { Injectable } from "@angular/core";
import * as _ from 'lodash';

@Injectable()
export class QueryModelService {
  createQueryModel(filterModel: FilterModel ): QueryModel {
    const queryModel: QueryModel = {
      year: filterModel.year,
      custLevel: filterModel.custLevel,
      username: filterModel.username,
      isDownline: filterModel.isDownline,
      currencies: filterModel.currencies,
      isAppliedVR: filterModel.isAppliedVR === 1,
      sports: filterModel.sports,
      leagueIds: filterModel.leagueIds,
      divisions: filterModel.divisions,
      accountStatus: filterModel.accountStatus,
      vrSettings: filterModel.vrSettings,
      pageIndex: filterModel.pageIndex,
      pageSize: filterModel.pageSize,
      permissionProfileId: filterModel.permissionProfileId,
      liveStatus: filterModel.liveStatus,
      betFromIds: filterModel.betFromIds
    };

    return queryModel;
  }

  createExcelQueryModel(tile: string, filterModel: FilterModel): ExcelQueryModel {
    const queryModel = this.createQueryModel(filterModel);
    queryModel.pageIndex = 0;
    queryModel.pageSize = 0;
    const excelQueryModel: ExcelQueryModel = _.extend({}, { title: tile }, queryModel);
    return excelQueryModel;
}
}
