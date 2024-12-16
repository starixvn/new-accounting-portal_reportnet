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
                    templateUrl: 'app/house-win-loss/house-win-loss.html',
                    controller: 'HouseWinlossController',
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