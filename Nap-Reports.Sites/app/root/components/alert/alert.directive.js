import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napAlert', napAlert);

    function napAlert() {
        var directive = {
            restrict: 'EA',
            scope: {},
            templateUrl: 'app/root/components/alert/alert.directive.html',
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$scope'];
    function controller($scope) {
        var vm = this;
        vm.isShow = false;
        vm.close = function () {
            vm.isShow = false;
        };

        $scope.$on('alert-show', function (event, message) {
            vm.message = message;
            vm.isShow = true;
        });

        $scope.$on('alert-hide', function () {
            vm.isShow = false;
        });
    }
})(angular);