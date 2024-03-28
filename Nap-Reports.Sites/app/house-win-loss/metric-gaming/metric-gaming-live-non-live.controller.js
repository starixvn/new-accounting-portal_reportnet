import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('MetricGamingLiveNonLiveController', MetricGamingLiveNonLiveController);
    MetricGamingLiveNonLiveController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService', 'titleService'];

    function MetricGamingLiveNonLiveController($scope, $element, fanexGridService, showHideColumnService, titleService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var betTypeGridElement = angular.element(gridElements[0]);
        var liveNonLiveGridElement = angular.element(gridElements[1]);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateBetType = generateBetType;
        $scope.generateMatchDetail = generateMatchDetail;
        $scope.generateSite = generateSite;
        $scope.viewSetting = {};

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            var betTypeGridOptions,
                liveNonLiveGridOptions;

            pane.service.getData(viewSetting, pane.data).then(function (data) {
                betTypeGridOptions = data[0];
                liveNonLiveGridOptions = data[1];   
                renderAll();
            });

            function renderAll() {
                var isDataReady = angular.isDefined(betTypeGridOptions) && angular.isDefined(liveNonLiveGridOptions);
                if (!isDataReady) {
                    return;
                }

                showHideColumnBuilder.build([betTypeGridElement, liveNonLiveGridElement], [betTypeGridOptions, liveNonLiveGridOptions]);

                createLinksForBetType(betTypeGridOptions);
                renderGridForBetType(betTypeGridOptions);
                createLinksForLiveNonLive(liveNonLiveGridOptions);
                renderGridForLiveNonLive(liveNonLiveGridOptions);
                pane.compileAll();
            }
        }

        function generateBetType(isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                isLive: isLive,
                sportTypeName: "Metric Gaming - Superlive Live"
            });
            tab.goForward('betType', data);
        }

        function generateMatchDetail(betType, betTypeName) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                sportTypeName: "Metric Gaming - Superlive Live",
                isLive: 2
            });
            tab.goForward('matchDetail', data);
        }

        function generateSite(betType, betTypeName, site, isHegde) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                siteType: site,
                mainColumnText: site,
                isHegde: isHegde
            });
            tab.goForward('site', data);
        }

        function renderGridForBetType(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(""),
                showCollapseExpandButton: true,
                showExcelButton: true,
                showButtonsWhenNoData: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.bottomBorderWidth = 3;
            betTypeGridElement.fanexGrid(data);
        }

        function renderGridForLiveNonLive(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" Live/Non-Live"),
                showCollapseExpandButton: true,
                showExcelButton: false
            };

            data.titleInfo = titleInfo;
            liveNonLiveGridElement.fanexGrid(data);
        }

        function createLinksForBetType(data) {
            var betTypeId;
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var betTypeName = betTypeRow.Columns[0].Value;
                var isHegde = betTypeName.match("hegde") ? true : false;
                betTypeId = betTypeRow.Columns[0].CustomAttr;
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\')">' + betTypeName + '</a>';
                for (var j = 0; j < betTypeRow.SubRows.length; j++) {
                    var siteRow = data.bodyRows[i].SubRows[j];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',\'' + siteName.replace(/'/g, "\\'") + '\',' + isHegde + ')">' + siteName + '</a>';
                }
            }
        }

        function createLinksForLiveNonLive(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var row = data.bodyRows[i];
                var name = row.Columns[0].Value;
                var isLive = parseInt(row.Columns[0].CustomAttr);
                row.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateBetType(' + isLive + ')">' + name + '</a>';
            }
        }

        function exportExcel() {
            var betTypeTitle = tab.generateBreadcrumbTextWithSuffix("");
            var liveNonLiveTitle = tab.generateBreadcrumbTextWithSuffix(" Live/Non-Live");

            var httpData = {};
            angular.extend(httpData, pane.data);
            angular.extend(httpData, {
                betTypeTitle: betTypeTitle,
                liveNonLiveTitle: liveNonLiveTitle
            });

            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }
    }
})(angular, undefined);