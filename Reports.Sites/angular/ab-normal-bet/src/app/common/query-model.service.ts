import { Injectable } from '@angular/core';
import * as _ from 'lodash';
import { QueryModel, ExcelQueryModel } from './model';
import { FilterModel } from '../filter/filter.model';

@Injectable()
export class QueryModelService {
    createQueryModel(filterModel: FilterModel): QueryModel {
        const queryModel: QueryModel = {
            fromDate: filterModel.fromDate,
            toDate: filterModel.toDate,
            licensee: filterModel.licensee.join(','),
            bettype: filterModel.bettype.join(','),
            sport: filterModel.sport.join(','),
            reason: filterModel.reason.join(','),
            isTest: filterModel.isTest,
            member: filterModel.member,
            leagueId: filterModel.leagueId,
            pageSize: filterModel.pageSize,
            pageIndex: filterModel.pageIndex,
            currencyId: filterModel.currencyId
        };

        return queryModel;
    }

    createExcelQueryModel(tile: string, filterModel: FilterModel): ExcelQueryModel {
        const queryModel = this.createQueryModel(filterModel);
        const excelQueryModel: ExcelQueryModel = _.extend({}, { title: tile }, queryModel);
        return excelQueryModel;
    }
}
