import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('NG2LiveNonLiveController', NG2LiveNonLiveController);

    NG2LiveNonLiveController.$inject = ['$scope', '$controller', '$element', 'cn88WinlossService', 'fanexGridService', 'showHideColumnService'];
    function NG2LiveNonLiveController($scope, $controller, $element, cn88WinlossService, fanexGridService, showHideColumnService) {
        angular.extend(this, $controller('BetTypeController', {
            $scope: $scope,
            $element: $element,
            cn88WinlossService: cn88WinlossService,
            fanexGridService: fanexGridService,
            showHideColumnService: showHideColumnService
        }));

        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var nonliveGridElement = angular.element(gridElements[0]);
        var liveGridElement = angular.element(gridElements[1]);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;


        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;

            var liveGridOptions, nonLiveGridOptions;
            var httpData = {};
            angular.extend(httpData, pane.data);
            httpData.isLive = 1;
            pane.service.getData(viewSetting, httpData).then(function (data) {
                liveGridOptions = data;
                renderAll();
            });

            httpData.isLive = 0;
            pane.service.getData(viewSetting, httpData).then(function (data) {
                nonLiveGridOptions = data;
                renderAll();
            });

            function renderAll() {
                var isDataReady = angular.isDefined(liveGridOptions) && angular.isDefined(nonLiveGridOptions);
                if (!isDataReady) {
                    return;
                }

                showHideColumnBuilder.build([liveGridElement, nonliveGridElement], [liveGridOptions, nonLiveGridOptions]);

                var canViewMatchDetail = httpData.endDate.diff(httpData.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    $scope.createDrilldownLinks(liveGridOptions.bodyRows, 1);
                    $scope.createDrilldownLinks(nonLiveGridOptions.bodyRows, 0);
                }

                renderLiveGrid(liveGridElement, liveGridOptions);
                renderNonLiveGrid(nonliveGridElement, nonLiveGridOptions);

                pane.compileAll();
            }
        }

        function renderNonLiveGrid(gridElement, data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" Non-Live"),
                showCollapseExpandButton: true,
                showExcelButton: true,
                showButtonsWhenNoData: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function renderLiveGrid(gridElement, data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" Live"),
                showCollapseExpandButton: true,
                showExcelButton: false
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function exportExcel() {

            var nonLiveTitle = tab.generateBreadcrumbTextWithSuffix(" Non-Live");
            var liveTitle = tab.generateBreadcrumbTextWithSuffix(" Live");
            var httpData = {};
            angular.extend(httpData, pane.data);
            angular.extend(httpData, { liveTitle: liveTitle, nonLiveTitle: nonLiveTitle });

            var serviceBaseUrl = "api/cn88-win-loss-by-products/ng2-live-non-live";
            var viewSetting = {};
            angular.extend(viewSetting, $scope.viewSetting);
            angular.extend(viewSetting, { serviceBaseUrl: serviceBaseUrl });

            pane.service.exportExcel(viewSetting, httpData, pane.getFileName());
        }
    }
})(angular, undefined);