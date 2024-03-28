import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .service('productShowHideColumnService', productShowHideColumnService);

    productShowHideColumnService.$inject = ['$log', '$timeout'];
    function productShowHideColumnService($log, $timeout) {
        var service = {
            defaultCollapseAllGroups: true,
            Builder: Builder
        };
        return service;

        function Builder(pane) {
            var defaultCollapseAllGroups = service.defaultCollapseAllGroups;
            var hiddenColumns = [1, 2];
            this.getButtonHtml = getButtonHtml;
            this.build = build;
            this.toggle = toggle;

            function toggle() {
                if (defaultCollapseAllGroups !== service.defaultCollapseAllGroups) {
                    var customButton = pane.$element.find('.btExpand').first();
                    $timeout(function () {
                        customButton.trigger('click');
                    });
                }
            }

            function getButtonHtml() {
                var buttonClass = !defaultCollapseAllGroups ? 'expanded' : 'collapsed';
                return '<input type="button" class="btExpand customized ' + buttonClass + ' nap-component" title="Collapse/Expand" data-ng-click="showHideColumns($event)" />';
            }

            function build(listOfGridElements, listOfGridOptions) {
                if (!angular.isArray(listOfGridElements)) {
                    listOfGridElements = [listOfGridElements];
                }
                if (!angular.isArray(listOfGridOptions)) {
                    listOfGridOptions = [listOfGridOptions];
                }
                if (listOfGridElements.length !== listOfGridOptions.length) {
                    $log.error('Invalid parameters');
                    return;
                }

                pane.$scope.showHideColumns = showHideColumns;
                for (var i = 0; i < listOfGridOptions.length; i++) {
                    var gridOptions = listOfGridOptions[i];
                    gridOptions.hiddenColumns = hiddenColumns;
                    gridOptions.enableHiddenColumns = !defaultCollapseAllGroups;
                    gridOptions.defaultCollapseAllGroups = defaultCollapseAllGroups;
                }

                function showHideColumns($event) {
                    defaultCollapseAllGroups = !defaultCollapseAllGroups;
                    service.defaultCollapseAllGroups = defaultCollapseAllGroups;
                    for (var i = 0; i < listOfGridOptions.length; i++) {
                        var gridElement = listOfGridElements[i];
                        var gridOptions = listOfGridOptions[i];
                        gridOptions.enableHiddenColumns = defaultCollapseAllGroups;
                        gridOptions.defaultCollapseAllGroups = true;
                        if (gridOptions.enableHiddenColumns) {
                            gridElement.fanexGrid("hideColumns", gridOptions, hiddenColumns);
                        } else {
                            gridElement.fanexGrid("hideColumns", gridOptions, []);
                        }
                    }

                    pane.compileDrilldown();
                    var target = angular.element($event.target);
                    if (!defaultCollapseAllGroups) {
                        target.removeClass('collapsed').addClass('expanded');
                    } else {
                        target.removeClass('expanded').addClass('collapsed');
                    }
                }
            }
        }
    }
})(angular);