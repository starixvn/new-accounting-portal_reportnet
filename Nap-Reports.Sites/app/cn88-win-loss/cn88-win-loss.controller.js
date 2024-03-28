import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('CN88WinlossController', CN88WinlossController);

    CN88WinlossController.$inject = [];
    function CN88WinlossController() {
        var vm = this;
        vm.panes = [];
    }
})(angular);