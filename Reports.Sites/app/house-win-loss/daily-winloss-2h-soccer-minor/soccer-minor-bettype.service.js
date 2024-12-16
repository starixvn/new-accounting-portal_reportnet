//import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('soccerMinorBetTypeService', soccerMinorBetTypeService);

    soccerMinorBetTypeService.$inject = ['$http', 'httpResponseService'];
    function soccerMinorBetTypeService($http, httpResponseService) {
        var service = {
            getData: getData,
            exportExcel: exportExcel
        };

        return service;

        function getData(viewSetting, data, isLive) {
            var httpData = {};
            angular.extend(httpData, data);
            angular.extend(httpData, viewSetting.mainParameters);
            httpData.isLive = isLive;

            var url = "api/house-win-loss-by-products/soccer-minor/bet-type";
            return $http.post(url, httpData)
                            .then(httpResponseService.getDataComplete)
                            .catch(httpResponseService.getDataFailed);
        }

        function exportExcel(viewSetting, data, fileName) {
            var url = 'api/house-win-loss-by-products/soccer-minor/bet-type-full/excel';
            var httpData = {};
            angular.extend(httpData, data);
            return $http.post(url, httpData, { responseType: "arraybuffer" })
                .then(function (response) {
                    httpResponseService.saveExcelFile(response.data, fileName);
                })
                .catch(httpResponseService.getDataFailed);
        }
    }
})(angular);