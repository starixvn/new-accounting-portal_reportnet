import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .factory('fanexGridService', fanexGridService);

    fanexGridService.$inject = [];
    function fanexGridService() {
        var service = {
            calculateExpandHeight: calculateExpandHeight
        };
        return service;

        function calculateExpandHeight(gridElement, hasTitle) {
            var bonusSpacing = hasTitle ? 25 : 5;
            var gridOffsetTop = gridElement.offset().top;
            return gridOffsetTop + bonusSpacing;
        }
    }
})(angular);