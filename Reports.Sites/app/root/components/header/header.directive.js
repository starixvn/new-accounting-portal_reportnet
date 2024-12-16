import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napHeader', napHeader);

    function napHeader() {
        var directive = {
            restrict: 'EA',
            scope: {},
            templateUrl: 'app/root/components/header/header.directive.html',
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['headerService', 'legacyService'];
    function controller(headerService, legacyService) {
        /*jshint validthis: true */
        var vm = this;
        vm.changePasswordUrl = legacyService.resolveUrl('/Auth/ChangePassword');
        vm.logOutUrl = legacyService.resolveUrl('/Auth/LogOut');
        vm.legacyUrl = napConfig.LegacyPath;

        headerService.getData().then(function (data) {
            vm.username = data.Username;
            vm.menus = headerService.getCompiledMenu(data.Menus);
        });
    }
})(angular);