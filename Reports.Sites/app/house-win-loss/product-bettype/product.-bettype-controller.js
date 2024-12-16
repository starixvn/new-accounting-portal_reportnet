import angular from "angular";
import numeral from "numeral";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('ProductBetTypeController', ProductBetTypeController);

    ProductBetTypeController.$inject = ['$element', '$scope', 'fanexGridService', 'houseWinlossService', 'productShowHideColumnService'];
    function ProductBetTypeController($element, $scope, fanexGridService, houseWinlossService, productShowHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new productShowHideColumnService.Builder(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.viewSetting = {};

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;

            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);
                renderGrid(data);
                pane.compileAll();
            });
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function exportExcel() {
            var title = tab.generateBreadcrumbText();
            var httpData = { title: title };
            angular.extend(httpData, pane.data);
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }
    }
})(angular);