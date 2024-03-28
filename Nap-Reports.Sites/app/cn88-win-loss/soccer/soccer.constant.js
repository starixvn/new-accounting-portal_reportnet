import angular from "angular";
(function (angular) {
    'use strict';

    var soccerConstants = {
        NonLive: 0,
        Live: 1,
        LiveNonLive: 2,
        SoccerSportType: 1
    };

    angular
        .module('app.cn88WinlossApp')
        .constant('soccerConstants', soccerConstants);
})(angular);