import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('breadcrumbService', breadcrumbService);

    breadcrumbService.$inject = [];
    function breadcrumbService() {
        var service = {
            generateHtml: generateHtml,
            generateText: generateText
        };
        return service;

        function generateHtml(list) {
            var html = '<ol class="breadcrumb">';
            for (var i = 0; i < list.length; i++) {
                var title = list[i];
                var isLast = i === list.length - 1;
                if (isLast) {
                    html += '<li class="active">' + title + '</li>';
                } else {
                    html += '<li><a href data-ng-click="tab.goBack(' + i + ')">' + title + '</a></li>';
                }
            }

            return html;
        }

        function generateText(list) {
            return list.join(' > ');
        }
    }
})(angular);