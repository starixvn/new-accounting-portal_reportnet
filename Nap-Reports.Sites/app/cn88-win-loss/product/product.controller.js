import angular from "angular";
import numeral from "numeral";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('ProductController', ProductController);

    ProductController.$inject = ['$element', '$scope', 'fanexGridService', 'cn88WinlossService', 'productShowHideColumnService'];
    function ProductController($element, $scope, fanexGridService, cn88WinlossService, productShowHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new productShowHideColumnService.Builder(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateProduct = generateProduct;
        $scope.viewSetting = {};

        function generateProduct(productName) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, { productName: productName });
            var level = cn88WinlossService.getMainView(productName);
            tab.goForward(level, data);
        }

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;

            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);
                createLinks(data);
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

        function createLinks(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var productRow = data.bodyRows[i];
                var customAttr = productRow.CustomAttr;
                if (customAttr === 'product') {
                    var productName = productRow.Columns[0].Value;
                    productRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProduct(\'' + productName.replace(/'/g, "\\'") + '\')">' + productName + '</a>';
                }
            }
        }

        function exportExcel() {
            var title = tab.generateBreadcrumbText();
            var httpData = { title: title };
            angular.extend(httpData, pane.data);
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }
    }
})(angular);