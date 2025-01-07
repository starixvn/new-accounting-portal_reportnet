import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('showHideColumnService', showHideColumnService);

    showHideColumnService.$inject = ['$log', '$timeout', 'productShowHideColumnService'];
    function showHideColumnService($log, $timeout, productShowHideColumnService) {
        function Builder(pane) {
            var defaultCollapseAllGroups = productShowHideColumnService.defaultCollapseAllGroups;
            this.getButtonHtml = getButtonHtml;
            this.build = build;
            this.toggle = toggle;

            function raiseDefaultClick() {
                var buttons = pane.$element.find('.btExpand');
                var customButton = buttons.first();
                if (!defaultCollapseAllGroups) {
                    customButton.removeClass('collapsed').addClass('expanded');
                } else {
                    customButton.removeClass('expanded').addClass('collapsed');
                }

                var defaultButtons = buttons.not(':eq(0)');
                $timeout(function () {
                    defaultButtons.trigger('click');
                });
            }

            function toggle() {
                if (defaultCollapseAllGroups != productShowHideColumnService.defaultCollapseAllGroups) {
                    defaultCollapseAllGroups = productShowHideColumnService.defaultCollapseAllGroups;
                    raiseDefaultClick();
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
                    gridOptions.defaultCollapseAllGroups = defaultCollapseAllGroups;
                }

                function showHideColumns($event) {
                    defaultCollapseAllGroups = !defaultCollapseAllGroups;
                    productShowHideColumnService.defaultCollapseAllGroups = defaultCollapseAllGroups;
                    raiseDefaultClick();
                }
            }
        };

        return Builder;
    }
})(angular);