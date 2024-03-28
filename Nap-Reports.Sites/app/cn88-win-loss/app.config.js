import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .config(config);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        configRouting();

        function configRouting() {
            $routeProvider
                .when('/', {
                    templateUrl: 'app/cn88-win-loss/cn88-win-loss.html',
                    controller: 'CN88WinlossController',
                    controllerAs: 'vm'
                })
                .when('/about', {
                    templateUrl: 'app/about/about.html',
                    controller: 'AboutController',
                    controllerAs: 'vm'
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    }
})(angular);