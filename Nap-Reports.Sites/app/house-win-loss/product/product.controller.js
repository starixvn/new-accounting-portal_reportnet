import angular from "angular";
import numeral from "numeral";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('ProductController', ProductController);

    ProductController.$inject = ['$element', '$scope', 'fanexGridService', 'houseWinlossService', 'productShowHideColumnService'];
    function ProductController($element, $scope, fanexGridService, houseWinlossService, productShowHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new productShowHideColumnService.Builder(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateProduct = generateProduct;
        $scope.generateProductBetTypes = generateProductBetTypes;
        $scope.viewSetting = {};

        function generateProduct(productName, productId) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, { productName: productName, aliasProductID: productId });
            var level = houseWinlossService.getMainView(productName);
            tab.goForward(level, data);
        }

        function generateProductBetTypes(productName, productId, siteType) {
            var isSabaSport = 2;

            if (productId === -1) {
                isSabaSport = 0;
            }

            if (productId === -2) {
                isSabaSport = 1;
            }

            if (productId === -3) {
                isSabaSport = 3;
            }

            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, { productName: productName, aliasProductID: productId, siteType: siteType, isLive: 2, isMajor: 2, isDrilldownFromMain: true, isSabaSport: isSabaSport });
            tab.goForward('productBetType', data);
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
                var productName = productRow.Columns[0].Value;
                var productId = parseInt(productRow.Columns[0].CustomAttr);
                if (customAttr === 'product') {
                    if(productName != 'SB Adjustment') {
                        productRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProduct(\'' + productName.replace(/'/g, "\\'") + '\',' + productId + '\)">' + productName + '</a>';
                    } else {
                        productRow.Columns[0].Value = productName;
                    }
                }

                if (productName === 'Soccer' ||
                    productName === 'SABA Soccer' ||
                    productName === 'SABA Soccer PinGoal' ||
                    productName === 'Metric Gaming - Superlive' ||
                    productName === 'Other Sports' ||
                    productName === 'Parlay' ||
                    productName === 'Bet Builder') {

                    if (productRow != null && productRow.SubRows != null) {
                        for (var j = 0; j < productRow.SubRows.length; j++) {
                            var subRow = productRow.SubRows[j];
                            var siteType = subRow.Columns[0].Value;
                            subRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProductBetTypes(\'' + productName.replace(/'/g, "\\'") + '\',' + productId + ',' + '\'' + siteType + '\')">' + siteType + '</a>';
                        }
                    }
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