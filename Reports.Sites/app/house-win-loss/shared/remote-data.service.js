import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('remoteDataService', remoteDataService);

    remoteDataService.$inject = ['$http', 'httpResponseService', 'houseWinlossService'];
    function remoteDataService($http, httpResponseService, houseWinlossService) {
        var service = {
            getData: getData,
            exportExcelForSingleGrid: exportExcelForSingleGrid,
            exportExcel: exportExcel
        };
        return service;

        function getData(viewSetting, data) {
            var httpData = {};
            angular.extend(httpData, data);
            angular.extend(httpData, viewSetting.mainParameters);

            return $http.post(viewSetting.serviceBaseUrl, httpData)
                .then(httpResponseService.getDataComplete)
                .catch(httpResponseService.getDataFailed);
        }

        function exportExcelForSingleGrid(viewSetting, tab, pane){
            var title = tab.generateBreadcrumbText();
            var httpData = {};
            angular.extend(httpData, pane.data);
            angular.extend(httpData, { title: title });

            exportExcel(viewSetting, httpData, pane.getFileName());
        }

        function exportExcel(viewSetting, data, fileName) {
            var httpData = {};
            angular.extend(httpData, data);
            angular.extend(httpData, viewSetting.mainParameters);

            var url = viewSetting.serviceBaseUrl + '/excel';
            return $http.post(url, httpData, { responseType: "arraybuffer" })
                .then(function (response) {
                    httpResponseService.saveExcelFile(response.data, fileName);
                })
                .catch(httpResponseService.getDataFailed);
        }
    }
})(angular);