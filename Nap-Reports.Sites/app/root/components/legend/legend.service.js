import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .factory('legendService', legendService);

    legendService.$inject = ['$http', 'httpResponseService', 'legacyService'];
    function legendService($http, httpResponseService, legacyService) {
        var service = {
            getData: getData
        };
        return service;

        function getData(menuId) {
            var url = legacyService.resolveUrl('/LegendManagement/LegendManagement/GetReportLegend');
            return $http.post(url, {
                menuId: menuId
            }).then(httpResponseService.getDataComplete);
        }
    }
})(angular);