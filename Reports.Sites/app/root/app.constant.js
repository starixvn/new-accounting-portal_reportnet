import angular from "angular";
import moment from "moment";
(function (angular, moment) {
    'use strict';

    angular
        .module('app.core')
        .constant('moment', moment)
        .constant('config', (function () {
            moment.now = function () {
                return napConfig.Today;
            };
            var today = moment.utc().startOf('day');
            var yesterday = moment(today).subtract(1, 'days');

            return {
                rootUrl: napConfig.RootUrl,
                legacyPath: napConfig.LegacyPath,
                today: today,
                yesterday: yesterday,
                balanceUpDate:  moment.utc(napConfig.BalanceUp).startOf('day')
            };
        })())
        .constant('formatStrings', {
            INT_NUMBER: '0,0',
            TWO_DECIMAL_NUMBER: '0,0.00',
            FOUR_DECIMAL_NUMBER: '0,0.0000',
            TWO_DECIMAL_PERCENT_NUMBER: '0,0.00%',
            MMM_DD_YYYY_STRING: 'MMM DD, YYYY',
            M_DD_YY_STRING: 'M dd, yy',
            MM_DD_YYYY_WITH_SLASH_STRING: 'MM/dd/yyyy',
            MMM_YY_STRING: 'MMM YY',
            MMM_YY_STRING_MONTH_PICKER: 'MMM yyyy',
            MMM_DD_YY_STRING_MONTH_PICKER: 'MMM dd, yyyy',
            MM_DD_YYYY_WITH_SLASH_STRING_MOMENT: 'M/D/YYYY',
            MMM_DD_YYYY_STRING_MOMENT: 'MMM D, YYYY',
            MMM_DD_YYYY_H_M_STRING: 'MMM DD, YYYY HH:mm',
            MMM_DD_YYYY_H_M_S_A_STRING: 'MMMM DD, YYYY h:mm:ss A',
            PERCENT_NUMBER: '0%'
        });
})(angular, moment);