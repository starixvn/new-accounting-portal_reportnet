import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .directive('napPane', napPane);

    napPane.$inject = ['$templateRequest', '$compile', '$controller'];
    function napPane($templateRequest, $compile, $controller) {
        var directive = {
            restrict: 'EA',
            require: '^^napTab',
            scope: {
                pane: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs, tabCtrl) {
            var pane = scope.pane;
            scope.tab = tabCtrl.tab;
            $templateRequest(pane.templateUrl, true).then(function (response) {
                var templateScope = scope.$new(false);
                var templateElement = element.html(response);
                var grids = templateElement.find('.freegrid');
                angular.forEach(grids, function (grid, key) {
                    var id = 'grid_' + templateScope.$id + '_' + key;
                    $(grid).attr('id', id);
                });
                var templateCtrl = $controller(pane.controller, {
                    $scope: templateScope,
                    $element: templateElement
                });

                templateElement.data('$ngControllerController', templateCtrl);
                $compile(templateElement.contents())(templateScope, function (clonedElement, scope) {
                    // save $scope & $element into pane
                    pane.$scope = scope;
                    pane.$element = templateElement;
                    pane.generate();
                });
            });
        }
    }
})(angular);