import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .factory('legacyService', legacyService);

    legacyService.$inject = ['config'];
    function legacyService(config) {
        var service = {
            legacyPath: config.legacyPath,
            resolveUrl: resolveUrl
        };
        return service;

        function resolveUrl(url) {
            if (config.legacyPath != null && config.legacyPath != "") {
                return config.legacyPath + url;
            } else {
                return url;
            }
            
        }
    }
})(angular);