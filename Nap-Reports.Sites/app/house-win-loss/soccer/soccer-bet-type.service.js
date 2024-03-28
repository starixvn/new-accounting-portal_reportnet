import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('soccerBetTypeService', soccerBetTypeService);

    soccerBetTypeService.$inject = ['$http', 'httpResponseService', 'soccerConstants'];
    function soccerBetTypeService($http, httpResponseService, soccerConstants) {
        var service = {
            getData: getData,
            exportExcel: exportExcel
        };

        return service;

        function getData(viewSetting, data) {
            var isMajor = buildMajorFlag(data);
            var isLive = data.isLive;

            var httpData = {
            };

            angular.extend(httpData, data);
            angular.extend(httpData, viewSetting.mainParameters);
            httpData.isMajor =isMajor;
            httpData.normalMode= data.normalMode;

            var url = buildUrl(isLive, isMajor);
            return $http.post(url, httpData)
                .then(httpResponseService.getDataComplete)
                .catch(httpResponseService.getDataFailed);
        }

        function exportExcel(title, data, fileName) {
            var isMajor = buildMajorFlag(data);
            var isLive = data.isLive;

            var httpData = {
                title: title,
                isMajor: isMajor,
                normalMode: data.normalMode
            };
            angular.extend(httpData, data);

            var url = buildUrl(isLive, isMajor) + '/excel';

            return $http.post(url, httpData, { responseType: "arraybuffer" })
                .then(function (response) {
                    httpResponseService.saveExcelFile(response.data, fileName);
                })
                .catch(httpResponseService.getDataFailed);
        }

        function buildMajorFlag(data)
        {
            var betTypeNameMajorMinor = data.betTypeNameMajorMinor;
            return betTypeNameMajorMinor === "Soccer-Major Bet Types" || betTypeNameMajorMinor === "SABA Soccer-Major Bet Types" ? true : false;
        }

        function buildUrl(isLive, isMajor)
        {
            var url = "api/house-win-loss-by-products/soccer/bettype";
            if (isMajor === 1 && isLive === soccerConstants.LiveNonLive) {
                url = "api/house-win-loss-by-products/soccer/sum-bettype";
            }
            else if (isMajor === 1 && isLive === soccerConstants.NonLive) {
                url = "api/house-win-loss-by-products/soccer/group-bettype";
            }

            return url;
        }
    }
})(angular);