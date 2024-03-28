import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('sportsbookMatchDetailService', sportsbookMatchDetailService);

    sportsbookMatchDetailService.$inject = ['$http', 'httpResponseService', 'legacyService', 'remoteDataService'];
    function sportsbookMatchDetailService($http, httpResponseService, legacyService, remoteDataService) {
        var service = {
            getMatchInformation: getMatchInformation,
            getData: remoteDataService.getData,
            exportExcel: remoteDataService.exportExcel
        };
        return service;

        function getMatchInformation(data) {
            var httpData = {
                matchId: data.matchId,
                betType: data.betType,
                sportType: data.sportType,
                reportDate: data.eventDate,
                matchCode: data.matchCode,
                liveIndicator: data.isLive
            };

            var url = legacyService.resolveUrl('/ScoreInformation/GetScoreInformation');
            return $http.post(url, httpData)
                .then(httpResponseService.getDataComplete)
                .catch(httpResponseService.getDataFailed);
        }
    }
})(angular);