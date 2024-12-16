import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napLegend', napLegend);

    function napLegend() {
        var directive = {
            restrict: 'EA',
            scope: {
                menuId: '='
            },
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$element', 'legendService'];
    function controller($element, legendService) {
        var vm = this;
        legendService.getData(vm.menuId).then(function (data) {
            $element.html(data.Content);
        });
    }
})(angular);