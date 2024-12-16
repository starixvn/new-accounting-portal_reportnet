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
            return config.legacyPath + url;
        }
    }
})(angular);