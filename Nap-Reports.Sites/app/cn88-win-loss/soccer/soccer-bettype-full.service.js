//import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .factory('soccerBetTypeFullService', soccerBetTypeFullService);

    soccerBetTypeFullService.$inject = ['$http', 'httpResponseService'];
    function soccerBetTypeFullService($http, httpResponseService) {
        var service = {
            getData: getData,
            exportExcel: exportExcel
        };

        return service;

        function getData(viewSetting, data, isLive) {
            var isMajor = data.betTypeNameMajorMinor === "Soccer-Major Bet Types";
            var isSumBetType = isMajor && isLive === 2;
            var isGroupBetType = isMajor && isLive === 0;
            var url = "api/cn88-win-loss-by-products/soccer/bettype";
            if (isSumBetType) {
                url = "api/cn88-win-loss-by-products/soccer/sum-bettype";
            }
            else if (isGroupBetType) {
                url = "api/cn88-win-loss-by-products/soccer/group-bettype";
            }

            var httpData = {};
            angular.extend(httpData, data);
            angular.extend(httpData, viewSetting.mainParameters);
            httpData.isLive = isLive;
            httpData.isMajor = isMajor;

            return $http.post(url, httpData)
                            .then(httpResponseService.getDataComplete)
                            .catch(httpResponseService.getDataFailed);
        }

        function exportExcel(viewSetting, data, fileName) {
            var isMajor = data.betTypeNameMajorMinor === "Soccer-Major Bet Types";
            var httpData = { isMajor: isMajor };
            angular.extend(httpData, data);

            var url = 'api/cn88-win-loss-by-products/soccer/bettype-full/excel';
            return $http.post(url, httpData, { responseType: "arraybuffer" })
                .then(function (response) {
                    httpResponseService.saveExcelFile(response.data, fileName);
                })
                .catch(httpResponseService.getDataFailed);
        }
    }
})(angular);