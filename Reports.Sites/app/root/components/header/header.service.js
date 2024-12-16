import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .factory('headerService', headerService);

    headerService.$inject = ['$http', 'httpResponseService', 'legacyService'];
    function headerService($http, httpResponseService, legacyService) {
        var service = {
            getData: getData,
            getCompiledMenu: getCompiledMenu,
            getMenuUrl: getMenuUrl
        };
        return service;

        function getCompiledMenu(menus) {
            for (var i = 0; i < menus.length; i++) {
                var menu = menus[i];

                if (menu.Children && menu.Children.length > 0) {
                    menu.Url = menu.Link;
                    for (var j = 0; j < menu.Children.length; j++) {
                        menu.Children[j].Url = getMenuUrl(menu.Children[j].Link);
                        menu.Children[j].Reload = location.href.indexOf(menu.Children[j].Link) >= 0;
                    }
                } else {
                    menu.Url = getMenuUrl(menu.Link);
                    menu.Reload = location.href.indexOf(menu.Link) >= 0;
                }


            }

            return menus;
        }

        function getData() {
            return $http.post('api/application/header', {})
                .then(httpResponseService.getDataComplete)
                .catch(httpResponseService.getDataFailed);
        }

        function getMenuUrl(url) {
            return isUniversalMenu(url)
            ? url
                : legacyService.resolveUrl(url);
        }

        function isUniversalMenu(url) {
            return url.indexOf("UniversalReports")>=0;
        }
    }
})(angular);