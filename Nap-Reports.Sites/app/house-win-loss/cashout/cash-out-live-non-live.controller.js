import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('CashOutLiveNonLiveController', CashOutLiveNonLiveController);
    CashOutLiveNonLiveController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService', 'titleService'];

    function CashOutLiveNonLiveController($scope, $element, fanexGridService, showHideColumnService, titleService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var betTypeGridElement = angular.element(gridElements[0]);
        var liveNonLiveGridElement = angular.element(gridElements[1]);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
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
        

        function generateMatchDetail(isLive, betTypeName, betTypeId) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                isLive: isLive,
                betTypeName: betTypeName,
                sportType: betTypeId
            });
            tab.goForward('matchDetail', data);
        }

        function generateSite(site, isLive, betTypeName, betTypeId) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                siteType: site,
                isLive: isLive,
                betTypeName: betTypeName,
                mainColumnText: site,
                sportType: betTypeId
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
            var isLive = 2;
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var betTypeName = betTypeRow.Columns[0].Value;
                var betTypeId = parseInt(betTypeRow.Columns[0].CustomAttr);
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + isLive + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',' + betTypeId + ')">' + betTypeName + '</a>';
                for (var j = 0; j < betTypeRow.SubRows.length; j++) {
                    var siteRow = data.bodyRows[i].SubRows[j];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + '\'' + siteName.replace(/'/g, "\\'") + '\',' + isLive + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',' + betTypeId + ')">' + siteName + '</a>';
                }
            }
        }

        function createLinksForLiveNonLive(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var row = data.bodyRows[i];
                var name = row.Columns[0].Value;
                var isLive = name.includes('Non-Live') === true ? 0 : 1;
                var betTypeId = parseInt(row.Columns[0].CustomAttr);
                row.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + isLive + ',\'' + name.replace(/'/g, "\\'") + '\',' + betTypeId + ')">' + name + '</a>';
                for (var j = 0; j < row.SubRows.length; j++) {
                    var siteRow = data.bodyRows[i].SubRows[j];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + '\'' + siteName.replace(/'/g, "\\'") + '\',' + isLive + ',\'' + name.replace(/'/g, "\\'") + '\',' + betTypeId + ')">' + siteName + '</a>';
                }
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