//import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('soccerBetTypeFullService', soccerBetTypeFullService);

    soccerBetTypeFullService.$inject = ['$http', 'httpResponseService'];
    function soccerBetTypeFullService($http, httpResponseService) {
        var service = {
            getData: getData,
            exportExcel: exportExcel
        };

        return service;

        function getData(viewSetting, data, isLive) {
            var betTypeNameMajorMinor = data.betTypeNameMajorMinor;
            var isMajor = betTypeNameMajorMinor === "Soccer-Major Bet Types"
                || betTypeNameMajorMinor === "SABA Soccer-Major Bet Types"
                || betTypeNameMajorMinor === "SABA Soccer PinGoal-Major Bet Types" ? 1 : 0;
            var isSumBetType = isMajor && isLive === 2;
            var isGroupBetType = isMajor && isLive === 0;
            var url = "api/house-win-loss-by-products/soccer/bettype";
            if (isSumBetType) {
                url = "api/house-win-loss-by-products/soccer/sum-bettype";
            }
            else if (isGroupBetType) {
                url = "api/house-win-loss-by-products/soccer/group-bettype";
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
            var betTypeNameMajorMinor = data.betTypeNameMajorMinor;
            var isMajor = betTypeNameMajorMinor === "Soccer-Major Bet Types"
                || betTypeNameMajorMinor === "SABA Soccer-Major Bet Types"
                || betTypeNameMajorMinor === "SABA Soccer PinGoal-Major Bet Types" ? 1 : 0;
            var httpData = { isMajor: isMajor };
            angular.extend(httpData, data);

            var url = 'api/house-win-loss-by-products/soccer/bettype-full/excel';
            return $http.post(url, httpData, { responseType: "arraybuffer" })
                .then(function (response) {
                    httpResponseService.saveExcelFile(response.data, fileName);
                })
                .catch(httpResponseService.getDataFailed);
        }
    }
})(angular);